using ErrorOr;
using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Persistence;

namespace OCPP.Core.WebApi.Services.ChargePoints;

public class ChargePointService : IChargePointService
{
  private readonly OCPPCoreDbContext _context;

  public ChargePointService(OCPPCoreDbContext context)
  {
    _context = context;
  }

  public ErrorOr<ChargePoint> AddChargePoint(ChargePoint chargepoint)
  {
    throw new NotImplementedException();
  }

  public ErrorOr<ChargePoint> GetChargePoint(int id)
  {
    throw new NotImplementedException();
  }

  public List<ChargePoint> GetChargePoints()
  {
    return _context.ChargePoints.ToList();
  }

  public ErrorOr<ChargePoint> UpsertChargePoint(ChargePoint chargepoint)
  {
    throw new NotImplementedException();
  }

  public ErrorOr<Deleted> DeleteChargepoint(int id)
  {
    throw new NotImplementedException();
  }


}