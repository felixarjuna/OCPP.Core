
using Microsoft.AspNetCore.Mvc;
using OCPP.Core.Contracts.Chargepoint;
using OCPP.Core.WebApi.Services.Chargepoints;

namespace OCPP.Core.WebApi.Controllers;

[Route("api/[controller]")]
public class ChargePointsController : ApiController
{
  private readonly IChargePointService _chargePointService;

  public ChargePointsController(IChargePointService chargePointService)
  {
    _chargePointService = chargePointService;
  }

  [HttpPost]
  public IActionResult CreateChargePoint(CreateChargePointRequest request)
  {
    return Ok();
  }
}