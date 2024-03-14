using OCPP.Core.Contracts.ChargeStation;
using OCPP.Core.Domain.Common.Enums;

namespace OCPP.Core.Domain.Entities;

public class ChargeStation
{
  public string StationId { get; set; } = null!;
  public string StationName { get; set; } = null!;

  public string ChargePointSerialNumber { get; set; } = null!;
  public string ChargeBoxSerialNumber { get; set; } = null!;
  public string Model { get; set; } = null!;
  public string VendorName { get; set; } = null!;
  public string FirmwareVersion { get; set; } = null!;
  public string MeterSerialNumber { get; set; } = null!;
  public ModemTypeEnum Modem { get; set; }
  public string MeterType { get; set; } = null!;

  public string? Username { get; set; }
  public string? Password { get; set; }
  public string? ClientCertThumb { get; set; }

  public string City { get; set; } = null!;
  public string Street { get; set; } = null!;

  public bool Online { get; set; }
  public string? Protocol { get; set; }

  public virtual List<Connector>? Connectors { get; set; }

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
    StationName = stationName;
    City = city;
    Street = street;
    Username = username;
    Password = password;
    ClientCertThumb = clientCertThumb;
  }

  private ChargeStation(
    string stationId,
    string stationName,
    string city,
    string street,
    string username,
    string password,
    string clientCertThumb,
    string model,
    string vendor,
    string chargePointSerialNumber,
    string chargeBoxSerialNumber,
    string firmwareVersion,
    string meterSerialNumber,
    string meterType
    )
  {
    StationId = stationId;
    StationName = stationName;
    City = city;
    Street = street;
    Username = username;
    Password = password;
    ClientCertThumb = clientCertThumb;
    Model = model;
    VendorName = vendor;
    ChargePointSerialNumber = chargePointSerialNumber;
    ChargeBoxSerialNumber = chargeBoxSerialNumber;
    Modem = ModemTypeEnum.iccid;
    FirmwareVersion = firmwareVersion;
    MeterSerialNumber = meterSerialNumber;
    MeterType = meterType;
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
      request.ClientCertThumb,
      request.Model,
      request.Vendor,
      request.ChargePointSerialNumber,
      request.ChargeBoxSerialNumber,
      request.FirmwareVersion,
      request.MeterSerialNumber,
      request.MeterType);
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
