using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using OCPP.Core.Database;

namespace OCPP.Core.Server;

/// <summary>
/// Encapsulates the data of a connected chargepoint in the server
/// </summary>
public class ChargePointStatus
{
  private Dictionary<int, OnlineConnectorStatus> _onlineConnectors;

  public ChargePointStatus()
  {
  }

  public ChargePointStatus(ChargeStation chargePoint)
  {
    Id = chargePoint.ChargeStationId;
    Name = chargePoint.Name;
  }

  /// <summary>
  /// ID of chargepoint
  /// </summary>
  [JsonProperty("id")]
  public string Id { get; set; }

  /// <summary>
  /// Name of chargepoint
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; set; }

  /// <summary>
  /// OCPP protocol version
  /// </summary>
  [JsonProperty("protocol")]
  public string Protocol { get; set; }

  /// <summary>
  /// Dictionary with online connectors
  /// </summary>
  public Dictionary<int, OnlineConnectorStatus> OnlineConnectors
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
  public WebSocket WebSocket { get; set; }
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

public enum ConnectorStatusEnum
{
  [EnumMember(Value = "")]
  Undefined = 0,

  [EnumMember(Value = "Available")]
  Available = 1,

  [EnumMember(Value = "Occupied")]
  Occupied = 2,

  [EnumMember(Value = "Unavailable")]
  Unavailable = 3,

  [EnumMember(Value = "Faulted")]
  Faulted = 4
}
