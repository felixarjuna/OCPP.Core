using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OCPP.Core.Domain.Common.Errors;
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
    if (_context.ChargePoints.Any(x => x.ChargePointId == chargepoint.ChargePointId))
      return Errors.ChargePoint.AlreadyExists;

    _context.ChargePoints.Add(chargepoint);
    _context.SaveChanges();
    return chargepoint;
  }

  public ErrorOr<ChargePoint> GetChargePoint(string id)
  {
    if (_context.ChargePoints.AsNoTracking().FirstOrDefault(x => x.ChargePointId == id) is not ChargePoint chargepoint)
      return Errors.ChargePoint.NotFound;
    return chargepoint;
  }

  public List<ChargePoint> GetChargePoints()
  {
    return _context.ChargePoints.ToList();
  }

  public ErrorOr<ChargePoint> UpsertChargePoint(ChargePoint chargepoint)
  {
    if (!_context.ChargePoints.Any(x => x.ChargePointId == chargepoint.ChargePointId))
      return Errors.ChargePoint.NotFound;

    _context.ChargePoints.Update(chargepoint);
    _context.SaveChanges();
    return chargepoint;
  }

  public ErrorOr<Deleted> DeleteChargepoint(string id)
  {
    if (_context.ChargePoints.FirstOrDefault(x => x.ChargePointId == id) is not ChargePoint chargepoint)
      return Errors.ChargePoint.NotFound;

    _context.ChargePoints.Remove(chargepoint);
    _context.SaveChanges();
    return Result.Deleted;
  }
}