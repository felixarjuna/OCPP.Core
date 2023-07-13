namespace OCPP.Core.WebApi.Services.Log;

public interface ILogService
{
  public void WriteMessageLog(
    string chargePointId,
    int? connectorId,
    string message,
    string? result,
    string errorCode);
}