#nullable disable
using OCPP.Core.Contracts.ChargeStation;

namespace OCPP.Core.Domain.Entities;

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

  private ChargeStation(
    string chargeStationId,
    string name,
    string comment,
    string username,
    string password,
    string clientCert)
  {
    ChargeStationId = chargeStationId;
    Name = name;
    Comment = comment;
    Username = username;
    Password = password;
    ClientCertThumb = clientCert;
  }

  public static ChargeStation Create(
    string chargeStationId,
    string Name,
    string Comment,
    string Username,
    string Password,
    string ClientCertThumb)
  {
    return new ChargeStation(
      chargeStationId,
      Name,
      Comment,
      Username,
      Password,
      ClientCertThumb);
  }

  public static ChargeStation From(CreateChargeStationRequest request)
  {
    return Create(
      request.ChargeStationId,
      request.Name,
      request.Comment,
      request.Username,
      request.Password,
      request.ClientCertThumb);
  }

  public static ChargeStation From(UpsertChargeStationRequest request)
  {
    return Create(
      request.ChargeStationId,
      request.Name,
      request.Comment,
      request.Username,
      request.Password,
      request.ClientCertThumb);
  }
}
