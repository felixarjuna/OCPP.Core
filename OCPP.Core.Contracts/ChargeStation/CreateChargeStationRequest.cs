namespace OCPP.Core.Contracts.ChargeStation;

public record CreateChargeStationRequest(
  string StationId,
  string StationName,
  string City,
  string Street,
  string Username,
  string Password,
  string ClientCertThumb,
  string Model,
  string Vendor,
  string ChargePointSerialNumber,
  string ChargeBoxSerialNumber,
  string FirmwareVersion,
  string MeterSerialNumber,
  string MeterType
);