using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OCPP.Core.Domain.Common.Errors;
using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Persistence;

namespace OCPP.Core.WebApi.Services.ChargeStations;

public class ChargeStationService : IChargeStationService
{
  private readonly OCPPCoreDbContext _context;

  public ChargeStationService(OCPPCoreDbContext context)
  {
    _context = context;
  }

  public ErrorOr<ChargeStation> AddChargeStation(ChargeStation ChargeStation)
  {
    if (_context.ChargeStations.Any(x => x.StationId == ChargeStation.StationId))
      return Errors.ChargeStation.AlreadyExists;

    _context.ChargeStations.Add(ChargeStation);
    _context.SaveChanges();
    return ChargeStation;
  }

  public ErrorOr<ChargeStation> GetChargeStationById(string id)
  {
    if (_context.ChargeStations.AsNoTracking().FirstOrDefault(x => x.StationId == id) is not ChargeStation ChargeStation)
      return Errors.ChargeStation.NotFound;
    return ChargeStation;
  }

  public List<ChargeStation> GetChargeStations()
  {
    return _context.ChargeStations.ToList();
  }

  public ErrorOr<ChargeStation> UpsertChargeStation(ChargeStation ChargeStation)
  {
    if (!_context.ChargeStations.Any(x => x.StationId == ChargeStation.StationId))
      return Errors.ChargeStation.NotFound;

    _context.ChargeStations.Update(ChargeStation);
    _context.SaveChanges();
    return ChargeStation;
  }

  public ErrorOr<Deleted> DeleteChargeStation(string id)
  {
    if (_context.ChargeStations.FirstOrDefault(x => x.StationId == id) is not ChargeStation ChargeStation)
      return Errors.ChargeStation.NotFound;

    _context.ChargeStations.Remove(ChargeStation);
    _context.SaveChanges();
    return Result.Deleted;
  }
}