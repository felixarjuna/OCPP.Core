namespace OCPP.Core.Contracts.Chargepoint;

public record UpsertChargePointRequest(
  string ChargePointId,
  string Name,
  string Comment,
  string Username,
  string Password,
  string ClientCertThumb
);