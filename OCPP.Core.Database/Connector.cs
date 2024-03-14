using System;
using System.Collections.Generic;
using OCPP.Core.Domain.Common.Enums;

namespace OCPP.Core.Database;

public class Connector
{
  public int ConnectorId { get; set; }
  public string ConnectorName { get; set; } = null!;

  public ConnectorStatusEnum Status { get; set; }

  public double? ChargeRateKW { get; set; }
  public double? MeterKWH { get; set; }
  public double? SoC { get; set; }

  public double? LastStatus { get; set; }
  public DateTime? LastStatusTime { get; set; }
  public double? LastMeter { get; set; }
  public DateTime? LastMeterTime { get; set; }

  // Navigation
  public int StationId { get; set; }
  public ChargeStation Station { get; set; } = null!;

  public virtual ICollection<Transaction>? Transactions { get; set; }
}
