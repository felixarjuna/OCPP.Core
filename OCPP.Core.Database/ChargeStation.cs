#nullable disable
using System.Collections.Generic;

namespace OCPP.Core.Database;

public partial class ChargeStation
{
  public ChargeStation() => Transactions = new HashSet<Transaction>();

  public string ChargeStationId { get; set; }
  public string Name { get; set; }
  public string Comment { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string ClientCertThumb { get; set; }

  public virtual ICollection<Transaction> Transactions { get; set; }
}
