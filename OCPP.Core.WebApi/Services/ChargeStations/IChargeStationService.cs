using ErrorOr;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.ChargeStations;

public interface IChargeStationService
{
  public ErrorOr<ChargeStation> AddChargeStation(ChargeStation ChargeStation);
  public List<ChargeStationResult> GetChargeStations();
  public ErrorOr<ChargeStation> GetChargeStationById(string id);
  public ErrorOr<ChargeStation> UpsertChargeStation(ChargeStation ChargeStation);
  public ErrorOr<Deleted> DeleteChargeStation(string id);
}