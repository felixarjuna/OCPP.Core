using System.Net.Cache;
using System.Net;
using OCPP.Core.Contracts.ChargeStation;
using OCPP.Core.Domain.Common.Enums;

namespace OCPP.Core.Domain.Entities;

public class ChargeStation
{
  public string StationId { get; set; } = null!;
  public string StationName { get; set; } = null!;

  public string? SerialNumber { get; set; }
  public string? Model { get; set; }
  public string? VendorName { get; set; }
  public string? FirmwareVersion { get; set; }
  public ModemTypeEnum Modem { get; set; }

  public string? Username { get; set; }
  public string? Password { get; set; }
  public string? ClientCertThumb { get; set; }

  public string City { get; set; } = null!;
  public string Street { get; set; } = null!;

  public bool Online { get; set; } = false;
  public string? Protocol { get; set; }

  public virtual ICollection<Connector>? Connectors { get; set; }

  private ChargeStation() { }

  private ChargeStation(
    string stationId,
    string stationName,
    string city,
    string street,
    string username,
    string password,
    string clientCertThumb)
  {
    StationId = stationId;
    StationId = stationName;
    City = city;
    Street = street;
    Username = username;
    Password = password;
    ClientCertThumb = clientCertThumb;
  }

  public static ChargeStation From(CreateChargeStationRequest request)
  {
    return new ChargeStation(
      request.StationId,
      request.StationName,
      request.City,
      request.Street,
      request.Username,
      request.Password,
      request.ClientCertThumb);
  }

  public static ChargeStation From(UpsertChargeStationRequest request)
  {
    return new ChargeStation(
      request.StationId,
      request.StationName,
      request.City,
      request.Street,
      request.Username,
      request.Password,
      request.ClientCertThumb);
  }
}
