using Mapster;
using OCPP.Core.Contracts.ChargeStation;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Common.Mapping;

public class ChargeStationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config
      .NewConfig<CreateChargeStationRequest, ChargeStation>()
      .MapWith(request => ChargeStation.From(request));

    config
      .NewConfig<UpsertChargeStationRequest, ChargeStation>()
      .MapWith(request => ChargeStation.From(request));
  }
}