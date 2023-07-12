using Mapster;
using OCPP.Core.Contracts.ChargeStation;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Common.Mapping;

public class ChargePointMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config
      .NewConfig<CreateChargeStationRequest, ChargeStation>()
      .MapWith(x => ChargeStation.From(x));

    config
      .NewConfig<UpsertChargeStationRequest, ChargeStation>()
      .MapWith(x => ChargeStation.From(x));
  }
}