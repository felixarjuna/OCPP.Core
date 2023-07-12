namespace OCPP.Core.WebApi.Services.Log;

public class LogService : ILogService
{
  public bool WriteMessageLog(
    string chargePointId,
    int? connectorId,
    string message,
    string result,
    string errorCode)
  {
    throw new NotImplementedException();
  }
}