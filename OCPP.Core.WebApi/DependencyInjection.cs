using System.Reflection;
using Mapster;
using MapsterMapper;
using OCPP.Core.WebApi.Services.ChargePoints;

namespace OCPP.Core.WebApi;

public static class DependencyInjection
{
  public static IServiceCollection AddServices(
    this IServiceCollection services)
  {
    services.AddMappings();
    services.AddScoped<IChargePointService, ChargePointService>();

    return services;
  }

  public static IServiceCollection AddMappings(this IServiceCollection services)
  {
    var config = TypeAdapterConfig.GlobalSettings;
    config.Scan(Assembly.GetExecutingAssembly());

    services.AddSingleton(config);
    services.AddScoped<IMapper, ServiceMapper>();

    return services;
  }
}