namespace OCPP.Core.Contracts.ChargeStation;

public record UpsertChargeStationRequest(
  string ChargeStationId,
  string Name,
  string Comment,
  string Username,
  string Password,
  string ClientCertThumb
);