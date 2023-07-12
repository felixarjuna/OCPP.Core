using System.Text;
using System.Net;
using ErrorOr;
using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Services.ChargeStations;
using System.Security.Cryptography.X509Certificates;
using OCPP.Core.WebApi.Common;
using System.Net.WebSockets;

namespace OCPP.Core.WebApi.Middlewares;
// #pragma warning disable // Disable all warnings
public class OCPPMiddleware
{
  private readonly RequestDelegate _next;

  public OCPPMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context, IChargeStationService _chargeStationService)
  {
    Console.WriteLine("OCPPMiddleware => Websocket request: Path='{0}'", context.Request.Path);

    // Retrieve charge station Id from request and check if Id exists
    if (!context.Request.Path.HasValue) return;
    string[] parts = context.Request.Path.Value.Split('/');
    string chargeStationId = string.IsNullOrWhiteSpace(parts[^1]) ? parts[^2] : parts[^1];

    Console.WriteLine("OCPPMiddleware => Connection request with chargepoint identifier = '{0}'", chargeStationId);

    ErrorOr<ChargeStation> result = _chargeStationService.GetChargeStationById(chargeStationId);
    if (result.IsError)
    {
      Console.WriteLine("OCPPMiddleware => FAILURE: Found no chargepoint with identifier={0}", chargeStationId);
      return;
    }

    ChargeStation station = result.Value;
    Console.WriteLine("OCPPMiddleware => SUCCESS: Found chargepoint with identifier={0}", station.ChargeStationId);

    // Message Authentication
    ChargePointStatus? status = OnMessageAuthentication(context, station);
    if (status is null)
    {
      context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
      return;
    }

    // WebSocket Request
    if (!context.WebSockets.IsWebSocketRequest)
    {
      Console.WriteLine("OCPPMiddleware => Non-WebSocket request.");
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
      return;
    }

    // Check protocols
    string? protocol = Defaults.SupportedProtocols.ToList().Find(context.WebSockets.WebSocketRequestedProtocols.Contains);
    if (string.IsNullOrEmpty(protocol))
    {
      Console.WriteLine("OCPPMiddleware => No supported sub-protocol from charge station '{1}'", station.ChargeStationId);
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
      return;
    }

    status.Protocol = protocol;

    // Handle socket communication
    Console.WriteLine("OCPPMiddleware => Waiting for message...");
    using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync(protocol);
    Console.WriteLine("OCPPMiddleware => WebSocket connection with charge station '{0}'", station.ChargeStationId);

    status.WebSocket = webSocket;

    // if (protocol == Defaults.Protocol_OCPP20) 
    //   await Receive20(status, context);
    // else if (protocol == Defaults.Protocol_OCPP16)
    //   await Receive16(status, context)

    await _next(context);
  }

  private static ChargePointStatus? OnMessageAuthentication(HttpContext context, ChargeStation station)
  {
    if (!string.IsNullOrWhiteSpace(station.Username))
    {
      // Charge station must send basic authorization header
      string? authHeader = context.Request.Headers["Authorization"];
      if (string.IsNullOrEmpty(authHeader))
      {
        context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"OCPP.Core\"");
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return default;
      }

      string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader[6..])).Split(':');
      if (station.Username != credentials[0]
        || station.Password != credentials[1])
      {
        // Authentication does NOT match => Failure
        Console.WriteLine("OCPPMiddleware => FAILURE: Basic authentication for chargepoint '{0}' does NOT match", station.ChargeStationId);
        context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"OCPP.Core\"");
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return default;
      }
      Console.WriteLine("OCPPMiddleware => SUCCESS: Basic authentication for chargepoint '{0}' match", station.ChargeStationId);
    }
    else if (!string.IsNullOrWhiteSpace(station.ClientCertThumb))
    {
      X509Certificate2? certificate = context.Connection.ClientCertificate;
      if (certificate is null)
      {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return default;
      }

      if (!certificate.Thumbprint.Equals(station.ClientCertThumb, StringComparison.InvariantCultureIgnoreCase))
      {
        Console.WriteLine("OCPPMiddleware => FAILURE: Certificate authentication for chargepoint '{0}' does NOT match", station.ChargeStationId);
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return default;
      }

      // Authentication match => OK
      Console.WriteLine("OCPPMiddleware => SUCCESS: Certificate authentication for chargepoint '{0}' match", station.ChargeStationId);
    }
    else
    {
      Console.WriteLine("OCPPMiddleware => No authentication for chargepoint '{0}' configured", station.ChargeStationId);
    }
    return new ChargePointStatus(station);
  }
}

public static class OCPPMiddlewareExtensions
{
  public static IApplicationBuilder UseOCPP(this IApplicationBuilder builder)
  {
    builder.Map("/OCPP", b => b.UseMiddleware<OCPPMiddleware>());
    return builder;
  }
}