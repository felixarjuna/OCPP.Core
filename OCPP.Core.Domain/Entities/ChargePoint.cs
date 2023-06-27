#nullable disable
using OCPP.Core.Contracts.Chargepoint;

namespace OCPP.Core.Domain.Entities;

public partial class ChargePoint
{
  public ChargePoint() => Transactions = new HashSet<Transaction>();

  public string ChargePointId { get; set; }
  public string Name { get; set; }
  public string Comment { get; set; }
  public string Username { get; set; }
  public string Password { get; set; }
  public string ClientCertThumb { get; set; }

  public virtual ICollection<Transaction> Transactions { get; set; }

  private ChargePoint(
    string chargePointId,
    string name,
    string comment,
    string username,
    string password,
    string clientCert)
  {
    ChargePointId = chargePointId;
    Name = name;
    Comment = comment;
    Username = username;
    Password = password;
    ClientCertThumb = clientCert;
  }

  public static ChargePoint Create(
    string ChargePointId,
    string Name,
    string Comment,
    string Username,
    string Password,
    string ClientCertThumb)
  {
    return new ChargePoint(
      ChargePointId,
      Name,
      Comment,
      Username,
      Password,
      ClientCertThumb);
  }

  public static ChargePoint From(CreateChargePointRequest request)
  {
    return Create(
      request.ChargePointId,
      request.Name,
      request.Comment,
      request.Username,
      request.Password,
      request.ClientCertThumb);
  }

  public static ChargePoint From(UpsertChargePointRequest request)
  {
    return Create(
      request.ChargePointId,
      request.Name,
      request.Comment,
      request.Username,
      request.Password,
      request.ClientCertThumb);
  }
}
