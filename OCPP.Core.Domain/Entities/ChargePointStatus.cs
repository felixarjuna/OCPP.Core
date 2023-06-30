using System.Net.WebSockets;
using Newtonsoft.Json;

namespace OCPP.Core.Domain.Entities;

/// <summary>
/// Encapsulates the data of a connected chargepoint in the server
/// </summary>
public class ChargePointStatus
{
  private Dictionary<int, OnlineConnectorStatus> _onlineConnectors;

  private ChargePointStatus() { }

  public ChargePointStatus(ChargePoint chargePoint)
  {
    Id = chargePoint.ChargePointId;
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
