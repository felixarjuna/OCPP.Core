#nullable disable
namespace OCPP.Core.Domain.Entities;

public partial class ChargeTag
{
  public string TagId { get; set; }
  public string TagName { get; set; }
  public string ParentTagId { get; set; }
  public DateTime? ExpiryDate { get; set; }
  public bool? Blocked { get; set; }
}
