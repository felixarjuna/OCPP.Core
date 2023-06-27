using ErrorOr;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.ChargePoints;

public interface IChargePointService
{
  public ErrorOr<ChargePoint> AddChargePoint(ChargePoint chargepoint);
  public List<ChargePoint> GetChargePoints();
  public ErrorOr<ChargePoint> GetChargePoint(string id);
  public ErrorOr<ChargePoint> UpsertChargePoint(ChargePoint chargepoint);
  public ErrorOr<Deleted> DeleteChargepoint(string id);
}