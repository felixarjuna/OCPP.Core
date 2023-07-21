using System.Net.WebSockets;
using Newtonsoft.Json;
using OCPP.Core.Domain.Common.Enums;

namespace OCPP.Core.Domain.Entities;

/// <summary>
/// Encapsulates the data of a connected chargepoint in the server
/// </summary>
public class ChargeStationStatus
{
  private Dictionary<int, OnlineConnectorStatus>? _onlineConnectors;

  public ChargeStationStatus() { }

  public ChargeStationStatus(ChargeStation chargePoint)
  {
    Id = chargePoint.StationId;
    Name = chargePoint.StationName;
  }

  /// <summary>
  /// ID of chargepoint
  /// </summary>
  [JsonProperty("id")]
  public string Id { get; set; } = null!;

  /// <summary>
  /// Name of chargepoint
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; set; } = null!;

  /// <summary>
  /// OCPP protocol version
  /// </summary>
  [JsonProperty("protocol")]
  public string? Protocol { get; set; }

  /// <summary>
  /// Dictionary with online connectors
  /// </summary>
  public Dictionary<int, OnlineConnectorStatus>? OnlineConnectors
  {
    get
    {
      return _onlineConnectors ??= new Dictionary<int, OnlineConnectorStatus>();
    }
    set
    {
      _onlineConnectors = value;
    }
  }

  /// <summary>
  /// WebSocket for internal processing
  /// </summary>
  [JsonIgnore]
  public WebSocket? WebSocket { get; set; }
}

/// <summary>
/// Encapsulates details about online charge point connectors
/// </summary>
public class OnlineConnectorStatus
{
  /// <summary>
  /// Status of charge connector
  /// </summary>
  public ConnectorStatusEnum Status { get; set; }

  /// <summary>
  /// Current charge rate in kW
  /// </summary>
  public double? ChargeRateKW { get; set; }

  /// <summary>
  /// Current meter value in kWh
  /// </summary>
  public double? MeterKWH { get; set; }

  /// <summary>
  /// StateOfCharges in percent
  /// </summary>
  public double? SoC { get; set; }
}
