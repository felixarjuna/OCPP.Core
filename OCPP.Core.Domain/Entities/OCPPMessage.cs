using Newtonsoft.Json;

namespace OCPP.Core.Domain.Entities;

/// <summary>
/// Wrapper object for OCPP Message
/// </summary>
public sealed class OCPPMessage
{
  /// <summary>
  /// Message type
  /// </summary>
  public string MessageType { get; set; } = null!;

  /// <summary>
  /// Message ID
  /// </summary>
  public string UniqueId { get; set; } = null!;

  /// <summary>
  /// Action
  /// </summary>
  public string Action { get; set; } = null!;

  /// <summary>
  /// JSON-Payload
  /// </summary>
  public string JsonPayload { get; set; } = null!;

  /// <summary>
  /// Error-Code
  /// </summary>
  public string? ErrorCode { get; set; }

  /// <summary>
  /// Error-Description
  /// </summary>
  public string? ErrorDescription { get; set; }

  /// <summary>
  /// TaskCompletionSource for asynchronous API result
  /// </summary>
  [JsonIgnore]
  public TaskCompletionSource<string>? TaskCompletionSource { get; set; }

  private OCPPMessage() { }

  private OCPPMessage(string messageType, string uniqueId, string action, string jsonPayload)
  {
    MessageType = messageType;
    UniqueId = uniqueId;
    Action = action;
    JsonPayload = jsonPayload;
  }

  public static OCPPMessage Create(string messageType, string uniqueId, string action, string jsonPayload)
  {
    return new OCPPMessage(messageType, uniqueId, action, jsonPayload);
  }

  public static OCPPMessage CreateResponse(string uniqueId)
  {
    return new OCPPMessage { UniqueId = uniqueId, MessageType = "3" };
  }
}