using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.OCPP;

public interface IOCPPService
{
  public OCPPMessage ProcessRequest(OCPPMessage messageIn);
  public void ProcessResponse(OCPPMessage messageIn, OCPPMessage messageOut);

  public void HandleReset(OCPPMessage messageIn, OCPPMessage messageOut);
  public void HandleUnlockConnector(OCPPMessage messageIn, OCPPMessage messageOut);

  public string HandleBootNotification(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleHeartBeat(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleAuthorize(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleStatusNotification(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleTransactionEvent(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleLogStatusNotification(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleClearedChargingLimit(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleDataTransfer(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleFirmwareStatusNotification(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleNotifyChargingLimit(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleNotifyEVChargingSchedule(OCPPMessage messageIn, OCPPMessage messageOut);
  public string HandleMeterValues(OCPPMessage messageIn, OCPPMessage messageOut);
}