using OCPP.Core.Domain.Common.Enums;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.ChargeStations;

public record ChargeStationResult(
  string StationId,
  string StationName,

  string? SerialNumber,
  string? Model,
  string? VendorName,
  ModemTypeEnum Modem,

  string? Username,
  string? Password,
  string? ClientCertThumb,

  string City,
  string Street,

  bool Online,
  string? Protocol,
  List<Connector>? Connectors,

  double? Energy,
  double? Money,
  int? Transactions
);