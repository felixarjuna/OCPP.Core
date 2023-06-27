
using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using OCPP.Core.Contracts.Chargepoint;
using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Services.ChargePoints;

namespace OCPP.Core.WebApi.Controllers;

[Route("api/[controller]")]
public class ChargePointsController : ApiController
{
  private readonly IChargePointService _chargePointService;
  private readonly IMapper _mapper;

  public ChargePointsController(IChargePointService chargePointService, IMapper mapper)
  {
    _chargePointService = chargePointService;
    _mapper = mapper;
  }

  [HttpPost]
  public IActionResult CreateChargePoint(CreateChargePointRequest request)
  {
    var chargepoint = _mapper.Map<ChargePoint>(request);
    ErrorOr<ChargePoint> result = _chargePointService.AddChargePoint(chargepoint);

    return result.Match(
      (res) => CreatedAtAction(
        actionName: "CreateChargePoint",
        routeValues: new { id = chargepoint.ChargePointId },
        value: res),
      (err) => Problem(err));
  }

  [HttpGet]
  public IActionResult GetAllChargePoints()
  {
    return Ok(_chargePointService.GetChargePoints());
  }

  [HttpGet("{id}")]
  public IActionResult GetChargePoint(int id)
  {
    return Ok(_chargePointService.GetChargePoint(id));
  }

  [HttpPut("{id}")]
  public IActionResult UpsertChargePoint(UpsertChargePointRequest request)
  {
    var chargepoint = _mapper.Map<ChargePoint>(request);
    ErrorOr<ChargePoint> result = _chargePointService.UpsertChargePoint(chargepoint);
    return result.Match(
      _ => NoContent(),
      (err) => Problem(err));
  }

  [HttpDelete("{id}")]
  public IActionResult DeleteChargePoint(int id)
  {
    ErrorOr<Deleted> result = _chargePointService.DeleteChargepoint(id);

    return result.Match(
      (res) => Ok(res),
      (err) => Problem(err)
    );
  }
}