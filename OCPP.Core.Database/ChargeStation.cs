using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OCPP.Core.Database;

public partial class ChargeStation
{
  public string StationId { get; set; } = null!;
  public string StationName { get; set; } = null!;

  public string ChargePointSerialNumber { get; set; } = null!;
  public string ChargeBoxSerialNumber { get; set; } = null!;
  public string Model { get; set; } = null!;
  public string VendorName { get; set; } = null!;
  public string FirmwareVersion { get; set; } = null!;
  public string MeterSerialNumber { get; set; } = null!;
  public string MeterType { get; set; } = null!;

  public string? Username { get; set; }
  public string? Password { get; set; }
  public string? ClientCertThumb { get; set; }

  public string City { get; set; } = null!;
  public string Street { get; set; } = null!;

  public bool Online { get; set; }
  public string? Protocol { get; set; }

  public virtual List<Connector>? Connectors { get; set; }

  public ChargeStation() => Transactions = new HashSet<Transaction>();

  public virtual ICollection<Transaction> Transactions { get; set; }
}
