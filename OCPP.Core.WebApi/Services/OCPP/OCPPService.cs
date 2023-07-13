using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Services.Log;
using OCPP.Protocol.OCPP20;

namespace OCPP.Core.WebApi.Services.OCPP;

public partial class OCPPService : OCPPBaseService, IOCPPService
{
  private readonly ILogService _logService;
  public const string VendorId = "Ploeg GmbH";

  public OCPPService(ILogService logService, ChargePointStatus chargePointStatus) : base(chargePointStatus)
  {
    _logService = logService;
  }

  public OCPPMessage ProcessRequest(OCPPMessage messageIn)
  {
    OCPPMessage msgOut = new()
    {
      MessageType = "3",
      UniqueId = messageIn.UniqueId
    };

    string errorCode;
    if (messageIn.MessageType == "2")
    {
      switch (messageIn.Action)
      {
        case "BootNotification":
          errorCode = HandleBootNotification(messageIn, msgOut);
          break;

        case "Heartbeat":
          errorCode = HandleHeartBeat(messageIn, msgOut);
          break;

        case "Authorize":
          errorCode = HandleAuthorize(messageIn, msgOut);
          break;

        case "TransactionEvent":
          errorCode = HandleTransactionEvent(messageIn, msgOut);
          break;

        case "MeterValues":
          errorCode = HandleMeterValues(messageIn, msgOut);
          break;

        case "StatusNotification":
          errorCode = HandleStatusNotification(messageIn, msgOut);
          break;

        case "DataTransfer":
          errorCode = HandleDataTransfer(messageIn, msgOut);
          break;

        case "LogStatusNotification":
          errorCode = HandleLogStatusNotification(messageIn, msgOut);
          break;

        case "FirmwareStatusNotification":
          errorCode = HandleFirmwareStatusNotification(messageIn, msgOut);
          break;

        case "ClearedChargingLimit":
          errorCode = HandleClearedChargingLimit(messageIn, msgOut);
          break;

        case "NotifyChargingLimit":
          errorCode = HandleNotifyChargingLimit(messageIn, msgOut);
          break;

        case "NotifyEVChargingSchedule":
          errorCode = HandleNotifyEVChargingSchedule(messageIn, msgOut);
          break;

        default:
          errorCode = ErrorCodes.NotSupported;
          _logService.WriteMessageLog("", null, messageIn.Action, messageIn.JsonPayload, errorCode);
          break;
      }
    }
    else
    {
      Console.WriteLine("ControllerOCPP20 => Protocol error: Wrong message type {0}", messageIn.MessageType);
      errorCode = ErrorCodes.ProtocolError;
    }

    if (!string.IsNullOrEmpty(errorCode))
    {
      // Invalid message type => return type "4" (CALLERROR)
      msgOut.MessageType = "4";
      msgOut.ErrorCode = errorCode;
      Console.WriteLine("ControllerOCPP20 => Return error code message: ErrorCode={0}", errorCode);
    }

    return msgOut;
  }

  public void ProcessResponse(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleAuthorize(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleBootNotification(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleClearedChargingLimit(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleDataTransfer(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleFirmwareStatusNotification(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleHeartBeat(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleLogStatusNotification(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleMeterValues(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleNotifyChargingLimit(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleNotifyEVChargingSchedule(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public void HandleReset(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleStatusNotification(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public string HandleTransactionEvent(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }

  public void HandleUnlockConnector(OCPPMessage messageIn, OCPPMessage messageOut)
  {
    throw new NotImplementedException();
  }


}