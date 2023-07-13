using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Common;
using OCPP.Core.WebApi.Common.Extensions;
using OCPP.Core.WebApi.Services.Log;
using OCPP.Core.WebApi.Services.OCPP;

namespace OCPP.Core.WebApi.Services.Messages;

public class MessageService : IMessageService
{
  private readonly ILogService _logService;

  public MessageService(ILogService logService)
  {
    _logService = logService;
  }

  // Dictionary for processing asynchronous API calls
  private readonly Dictionary<string, OCPPMessage> _requestQueue = new();

  public Task ReceiveOCPP16(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task ResetOCPP16(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task UnlockConnectorOCPP16(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task SendMessageOCPP16(OCPPMessage message, WebSocket webSocket)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Waits for new OCPP V2.0 messages on the open websocket connection and delegates processing to a controller
  /// </summary>
  public async Task ReceiveOCPP20(ChargePointStatus status)
  {
    byte[] buffer = new byte[1024 * 4];
    MemoryStream memStream = new(buffer.Length);

    OCPPService _ocppService = new(_logService, status);

    while (status.WebSocket.State == WebSocketState.Open)
    {
      WebSocketReceiveResult result = await status.WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
      if (result is null || result.MessageType == WebSocketMessageType.Close)
      {
        Console.WriteLine("OCPPMiddleware.Receive20 => Receive: unexpected result: CloseStatus={0} / MessageType={1}", result?.CloseStatus, result?.MessageType);
      }
      else
      {
        Console.WriteLine("OCPPMiddleware.Receive20 => Receiving segment: {0} bytes (EndOfMessage={1} / MsgType={2})", result.Count, result.EndOfMessage, result.MessageType);
        memStream.Write(buffer, 0, result.Count);

        // Only receive end of message
        if (!result.EndOfMessage) continue;

        // Read complete message into byte array
        byte[] bMessage = memStream.ToArray();
        //  memory stream für next message
        memStream = new MemoryStream(buffer.Length);

        string ocppMessage = Encoding.UTF8.GetString(bMessage);
        Match match = DefaultRegex.OCPPMessage.Match(ocppMessage);
        if (match.Groups.Count >= 3)
        {
          var (messageTypeId, uniqueId, action, jsonPayload) = match.Groups;

          Console.WriteLine("OCPPMiddleware.Receive20 => OCPP-Message: Type={0} / ID={1} / Action={2})", messageTypeId, uniqueId, action);

          OCPPMessage messageIn = new(messageTypeId, uniqueId, action, jsonPayload);
          switch (messageIn.MessageType)
          {
            // Handle request from charge station
            case "2":
              OCPPMessage msgOut = _ocppService.ProcessRequest(messageIn);

              // Send OCPP message with optional logging/dump
              await SendMessageOCPP20(msgOut, status.WebSocket);
              break;
            // Handle response from charge station
            case "3":
            case "4":
              if (_requestQueue.ContainsKey(messageIn.UniqueId))
              {
                _ocppService.ProcessResponse(messageIn, _requestQueue[messageIn.UniqueId]);
                _requestQueue.Remove(messageIn.UniqueId);
              }
              else
              {
                Console.WriteLine("OCPPMiddleware.Receive20 => HttpContext from caller not found / Msg: {0}", ocppMessage);
              }
              break;
            default:
              // Unknown message type
              Console.WriteLine("OCPPMiddleware.Receive20 => Unknown message type: {0} / Msg: {1}", messageIn.MessageType, ocppMessage);
              break;
          }
        }
      }
    }

    Console.WriteLine("OCPPMiddleware.Receive20 => Websocket closed: State={0} / CloseStatus={1}", status.WebSocket.State, status.WebSocket.CloseStatus);
  }

  public Task ResetOCPP20(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task SendMessageOCPP20(OCPPMessage message, WebSocket webSocket)
  {
    throw new NotImplementedException();
  }

  public Task UnlockConnectorOCPP20(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }
}
