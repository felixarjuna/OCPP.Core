using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.Chargepoints;

public interface IChargePointService
{
  public void AddChargePoint(ChargePoint chargepoint);
  public void GetChargePoints();
  public void GetChargePoint(int id);
  public void DeleteChargepoint(int id);
}