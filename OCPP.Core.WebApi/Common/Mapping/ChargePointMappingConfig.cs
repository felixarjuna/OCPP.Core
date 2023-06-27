using Mapster;
using OCPP.Core.Contracts.Chargepoint;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Common.Mapping;

public class ChargePointMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config
      .NewConfig<CreateChargePointRequest, ChargePoint>()
      .MapWith(x => ChargePoint.From(x));

    config
      .NewConfig<UpsertChargePointRequest, ChargePoint>()
      .MapWith(x => ChargePoint.From(x));
  }
}