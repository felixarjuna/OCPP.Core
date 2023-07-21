using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using OCPP.Core.Contracts.ChargeStation;
using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Services.ChargeStations;

namespace OCPP.Core.WebApi.Controllers;

[Route("api/[controller]")]
public class ChargeStationsController : ApiController
{
  private readonly IChargeStationService _chargeStationService;
  private readonly IMapper _mapper;

  public ChargeStationsController(IChargeStationService ChargeStationService, IMapper mapper)
  {
    _chargeStationService = ChargeStationService;
    _mapper = mapper;
  }

  [HttpPost]
  public IActionResult CreateChargeStation([FromBody] CreateChargeStationRequest request)
  {
    var station = _mapper.Map<ChargeStation>(request);

    ErrorOr<ChargeStation> result = _chargeStationService.AddChargeStation(station);
    return result.Match(
      (res) => CreatedAtAction(
        actionName: "CreateChargeStation",
        routeValues: new { id = station.StationId },
        value: res),
      (err) => Problem(err));
  }

  [HttpGet]
  public IActionResult GetAllChargeStations()
  {
    return Ok(_chargeStationService.GetChargeStations());
  }

  [HttpGet("{id}")]
  public IActionResult GetChargeStation(string id)
  {
    ErrorOr<ChargeStation> result = _chargeStationService.GetChargeStationById(id);

    return result.Match(
      (res) => Ok(res),
      (err) => Problem(err));
  }

  [HttpPut("{id}")]
  public IActionResult UpsertChargeStation(string id, [FromBody] UpsertChargeStationRequest request)
  {
    if (id != request.StationId) return BadRequest();

    var ChargeStation = _mapper.Map<ChargeStation>(request);

    ErrorOr<ChargeStation> result = _chargeStationService.UpsertChargeStation(ChargeStation);
    return result.Match(
      _ => NoContent(),
      (err) => Problem(err));
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteChargeStation(string id)
  {
    ErrorOr<Deleted> result = _chargeStationService.DeleteChargeStation(id);

    return result.Match(
      (_) => NoContent(),
      (err) => Problem(err)
    );
  }
}