namespace OCPP.Core.Contracts.ChargeStation;

public record CreateChargeStationRequest(
  string ChargeStationId,
  string Name,
  string Comment,
  string Username,
  string Password,
  string ClientCertThumb
);