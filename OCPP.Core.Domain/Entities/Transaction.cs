namespace OCPP.Core.Domain.Entities;

public class Transaction
{
  public int TransactionId { get; set; }

  public string Uid { get; set; } = null!;
  public string StartTagId { get; set; } = null!;
  public DateTime StartTime { get; set; }
  public double MeterStart { get; set; }
  public string StartResult { get; set; } = null!;
  public string StopTagId { get; set; } = null!;
  public DateTime? StopTime { get; set; }
  public double? MeterStop { get; set; }
  public string StopReason { get; set; } = null!;

  // Navigation
  public int ConnectorId { get; set; }
  public virtual Connector Connector { get; set; } = null!;
}
