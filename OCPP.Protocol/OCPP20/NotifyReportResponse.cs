#pragma warning disable // Disable all warnings
using Newtonsoft.Json;

namespace OCPP.Protocol.OCPP20;

/// <summary>
/// This file was automatically generated by json-schema-to-typescript.
/// DO NOT MODIFY IT BY HAND. Instead, modify the source JSONSchema file,
/// and run json-schema-to-typescript to regenerate this file.
/// </summary>
public class NotifyReportResponse
{
  [JsonProperty("customData", NullValueHandling = NullValueHandling.Ignore)]
  public CustomDataType? CustomData { get; set; }
}