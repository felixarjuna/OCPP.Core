namespace OCPP.Core.Domain.Entities;

public sealed partial class MessageLog
{
  public int LogId { get; set; }
  public DateTime LogTime { get; set; }
  public string ChargePointId { get; set; } = null!;
  public int? ConnectorId { get; set; }
  public string Message { get; set; } = null!;
  public string? Result { get; set; }
  public string ErrorCode { get; set; } = null!;

  private MessageLog() { }

  public MessageLog(DateTime logTime, string chargeStationId, int? connectorId, string message, string? result, string errorCode)
  {
    LogTime = logTime;
    ChargePointId = chargeStationId;
    ConnectorId = connectorId;
    Message = message;
    Result = result;
    ErrorCode = errorCode;
  }

  public static MessageLog Create(DateTime logTime, string chargeStationId, int? connectorId, string message, string? result, string errorCode)
  {
    return new MessageLog(
      logTime,
      chargeStationId,
      connectorId,
      message,
      result,
      errorCode);
  }
}
