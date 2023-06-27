namespace OCPP.Core.Contracts.Chargepoint;

public record CreateChargePointRequest(
  string ChargePointId,
  string Name,
  string Comment,
  string Username,
  string Password,
  string ClientCertThumb
);