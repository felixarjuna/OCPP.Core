using OCPP.Core.Domain.Common.Enums;

namespace OCPP.Core.Domain.Entities;

/// <summary>
/// Encapsulates details about online charge point connectors
/// </summary>
public class OnlineConnectorStatus
{
  /// <summary>
  /// Status of charge connector
  /// </summary>
  public ConnectorStatusEnum Status { get; set; }

  /// <summary>
  /// Current charge rate in kW
  /// </summary>
  public double? ChargeRateKW { get; set; }

  /// <summary>
  /// Current meter value in kWh
  /// </summary>
  public double? MeterKWH { get; set; }

  /// <summary>
  /// StateOfCharges in percent
  /// </summary>
  public double? SoC { get; set; }
}