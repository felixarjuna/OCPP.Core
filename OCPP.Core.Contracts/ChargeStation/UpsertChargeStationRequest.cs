namespace OCPP.Core.Contracts.ChargeStation;

public record UpsertChargeStationRequest(
  string StationId,
  string StationName,
  string City,
  string Street,
  string Username,
  string Password,
  string ClientCertThumb
);