using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OCPP.Core.Database;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace OCPP.Core.Server;

public partial class OCPPMiddleware
{
  /// <summary>
  /// Waits for new OCPP V1.6 messages on the open websocket connection and delegates processing to a controller
  /// </summary>
  private async Task Receive16(ChargePointStatus chargePointStatus, HttpContext context)
  {
    ILogger logger = _logFactory.CreateLogger("OCPPMiddleware.OCPP16");
    ControllerOCPP16 controller16 = new(_configuration, _logFactory, chargePointStatus);

    byte[] buffer = new byte[1024 * 4];
    MemoryStream memStream = new(buffer.Length);

    while (chargePointStatus.WebSocket.State == WebSocketState.Open)
    {
      WebSocketReceiveResult result = await chargePointStatus.WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
      if (result != null && result.MessageType != WebSocketMessageType.Close)
      {
        logger.LogTrace("OCPPMiddleware.Receive16 => Receiving segment: {0} bytes (EndOfMessage={volt1} / MsgType={2})", result.Count, result.EndOfMessage, result.MessageType);
        memStream.Write(buffer, 0, result.Count);

        if (result.EndOfMessage)
        {
          // read complete message into byte array
          byte[] bMessage = memStream.ToArray();
          // reset memory stream für next message
          memStream = new MemoryStream(buffer.Length);

          string dumpDir = _configuration.GetValue<string>("MessageDumpDir");
          if (!string.IsNullOrWhiteSpace(dumpDir))
          {
            string path = Path.Combine(dumpDir, string.Format("{0}_ocpp16-in.txt", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ffff")));
            try
            {
              // Write incoming message into dump directory
              File.WriteAllBytes(path, bMessage);
            }
            catch (Exception exp)
            {
              logger.LogError(exp, "OCPPMiddleware.Receive16 => Error dumping incoming message to path: '{0}'", path);
            }
          }

          string ocppMessage = UTF8Encoding.UTF8.GetString(bMessage);

          Match match = Regex.Match(ocppMessage, MessageRegExp);
          if (match != null && match.Groups != null && match.Groups.Count >= 3)
          {
            string messageTypeId = match.Groups[1].Value;
            string uniqueId = match.Groups[2].Value;
            string action = match.Groups[3].Value;
            string jsonPayload = match.Groups[4].Value;
            logger.LogInformation("OCPPMiddleware.Receive16 => OCPP-Message: Type={0} / ID={1} / Action={2})", messageTypeId, uniqueId, action);

            OCPPMessage msgIn = new(messageTypeId, uniqueId, action, jsonPayload);
            if (msgIn.MessageType == "2")
            {
              // Request from chargepoint to OCPP server
              OCPPMessage msgOut = controller16.ProcessRequest(msgIn);

              // Send OCPP message with optional logging/dump
              await SendOcpp16Message(msgOut, logger, chargePointStatus.WebSocket);
            }
            else if (msgIn.MessageType == "3" || msgIn.MessageType == "4")
            {
              // Process answer from chargepoint
              if (_requestQueue.ContainsKey(msgIn.UniqueId))
              {
                controller16.ProcessAnswer(msgIn, _requestQueue[msgIn.UniqueId]);
                _requestQueue.Remove(msgIn.UniqueId);
              }
              else
              {
                logger.LogError("OCPPMiddleware.Receive16 => HttpContext from caller not found / Msg: {0}", ocppMessage);
              }
            }
            else
            {
              // Unknown message type
              logger.LogError("OCPPMiddleware.Receive16 => Unknown message type: {0} / Msg: {1}", msgIn.MessageType, ocppMessage);
            }
          }
          else
          {
            logger.LogWarning("OCPPMiddleware.Receive16 => Error in RegEx-Matching: Msg={0})", ocppMessage);
          }
        }
      }
      else
      {
        logger.LogInformation("OCPPMiddleware.Receive16 => WebSocket Closed: CloseStatus={0} / MessageType={1}", result?.CloseStatus, result?.MessageType);
        await chargePointStatus.WebSocket.CloseOutputAsync((WebSocketCloseStatus)3001, string.Empty, CancellationToken.None);
      }
    }
    logger.LogInformation("OCPPMiddleware.Receive16 => Websocket closed: State={0} / CloseStatus={1}", chargePointStatus.WebSocket.State, chargePointStatus.WebSocket.CloseStatus);
    _chargePointStatusDict.Remove(chargePointStatus.Id, out ChargePointStatus dummy);

    using OCPPCoreContext dbContext = new(_configuration);
    ChargeStation chargeStation = dbContext.Find<ChargeStation>(chargePointStatus.Id); ;
    chargeStation.Online = false;
    dbContext.SaveChanges();
  }

  /// <summary>
  /// Waits for new OCPP V1.6 messages on the open websocket connection and delegates processing to a controller
  /// </summary>
  private async Task Reset16(ChargePointStatus chargePointStatus, HttpContext apiCallerContext)
  {
    ILogger logger = _logFactory.CreateLogger("OCPPMiddleware.OCPP16");
    ControllerOCPP16 controller16 = new(_configuration, _logFactory, chargePointStatus);

    Messages_OCPP16.ResetRequest resetRequest = new()
    {
      Type = Messages_OCPP16.ResetRequestType.Soft
    };
    string jsonResetRequest = JsonConvert.SerializeObject(resetRequest);

    OCPPMessage msgOut = new()
    {
      MessageType = "2",
      Action = "Reset",
      UniqueId = Guid.NewGuid().ToString("N"),
      JsonPayload = jsonResetRequest,
      TaskCompletionSource = new TaskCompletionSource<string>()
    };

    // store HttpContext with MsgId for later answer processing (=> send answer to API caller)
    _requestQueue.Add(msgOut.UniqueId, msgOut);

    // Send OCPP message with optional logging/dump
    await SendOcpp16Message(msgOut, logger, chargePointStatus.WebSocket);

    // Wait for asynchronous chargepoint response and processing
    string apiResult = await msgOut.TaskCompletionSource.Task;

    //
    apiCallerContext.Response.StatusCode = 200;
    apiCallerContext.Response.ContentType = "application/json";
    await apiCallerContext.Response.WriteAsync(apiResult);
  }

  /// <summary>
  /// Sends a Unlock-Request to the chargepoint
  /// </summary>
  private async Task UnlockConnector16(ChargePointStatus chargePointStatus, HttpContext apiCallerContext)
  {
    ILogger logger = _logFactory.CreateLogger("OCPPMiddleware.OCPP16");
    ControllerOCPP16 controller16 = new(_configuration, _logFactory, chargePointStatus);

    Messages_OCPP16.UnlockConnectorRequest unlockConnectorRequest = new()
    {
      ConnectorId = 0
    };

    string jsonResetRequest = JsonConvert.SerializeObject(unlockConnectorRequest);

    OCPPMessage msgOut = new()
    {
      MessageType = "2",
      Action = "UnlockConnector",
      UniqueId = Guid.NewGuid().ToString("N"),
      JsonPayload = jsonResetRequest,
      TaskCompletionSource = new TaskCompletionSource<string>()
    };

    // store HttpContext with MsgId for later answer processing (=> send answer to API caller)
    _requestQueue.Add(msgOut.UniqueId, msgOut);

    // Send OCPP message with optional logging/dump
    await SendOcpp16Message(msgOut, logger, chargePointStatus.WebSocket);

    // Wait for asynchronous chargepoint response and processing
    string apiResult = await msgOut.TaskCompletionSource.Task;

    //
    apiCallerContext.Response.StatusCode = 200;
    apiCallerContext.Response.ContentType = "application/json";
    await apiCallerContext.Response.WriteAsync(apiResult);
  }

  private async Task SendOcpp16Message(OCPPMessage msg, ILogger logger, WebSocket webSocket)
  {
    string ocppTextMessage;
    if (string.IsNullOrEmpty(msg.ErrorCode))
    {
      if (msg.MessageType == "2")
      {
        // OCPP-Request
        ocppTextMessage = string.Format("[{0},\"{1}\",\"{2}\",{3}]", msg.MessageType, msg.UniqueId, msg.Action, msg.JsonPayload);
      }
      else
      {
        // OCPP-Response
        ocppTextMessage = string.Format("[{0},\"{1}\",{2}]", msg.MessageType, msg.UniqueId, msg.JsonPayload);
      }
    }
    else
    {
      ocppTextMessage = string.Format("[{0},\"{1}\",\"{2}\",\"{3}\",{4}]", msg.MessageType, msg.UniqueId, msg.ErrorCode, msg.ErrorDescription, "{}");
    }
    logger.LogTrace("OCPPMiddleware.OCPP16 => SendOcppMessage: {0}", ocppTextMessage);

    if (string.IsNullOrEmpty(ocppTextMessage))
    {
      // invalid message
      ocppTextMessage = string.Format("[{0},\"{1}\",\"{2}\",\"{3}\",{4}]", "4", string.Empty, Messages_OCPP16.ErrorCodes.ProtocolError, string.Empty, "{}");
    }

    string dumpDir = _configuration.GetValue<string>("MessageDumpDir");
    if (!string.IsNullOrWhiteSpace(dumpDir))
    {
      // Write outgoing message into dump directory
      string path = Path.Combine(dumpDir, string.Format("{0}_ocpp16-out.txt", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ffff")));
      try
      {
        File.WriteAllText(path, ocppTextMessage);
      }
      catch (Exception exp)
      {
        logger.LogError(exp, "OCPPMiddleware.SendOcpp16Message=> Error dumping message to path: '{0}'", path);
      }
    }

    byte[] binaryMessage = Encoding.UTF8.GetBytes(ocppTextMessage);
    await webSocket.SendAsync(new ArraySegment<byte>(binaryMessage, 0, binaryMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
  }
}
