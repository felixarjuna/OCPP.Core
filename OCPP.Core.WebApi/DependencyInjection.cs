using System.Reflection;
using Mapster;
using MapsterMapper;
using OCPP.Core.WebApi.Services.ChargeStations;
using OCPP.Core.WebApi.Services.Log;
using OCPP.Core.WebApi.Services.Messages;

namespace OCPP.Core.WebApi;

public static class DependencyInjection
{
  public static IServiceCollection AddServices(
    this IServiceCollection services)
  {
    services.AddMappings();
    services.AddScoped<IChargeStationService, ChargeStationService>();
    services.AddScoped<IMessageService, MessageService>();
    services.AddScoped<ILogService, LogService>();

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