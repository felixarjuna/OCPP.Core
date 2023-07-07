using Newtonsoft.Json;

#pragma warning disable // Disable all warnings
namespace OCPP.Protocol;

/// <summary>
/// This class does not get 'AdditionalProperties = false' in the schema generation, so it can be extended with arbitrary JSON properties to allow adding custom data.
/// </summary>
public class CustomDataType
{
  [JsonProperty("vendorId", NullValueHandling = NullValueHandling.Ignore)]
  string VendorId { get; set; }

  /// <summary>
  /// This class does not get 'AdditionalProperties = false' in the schema generation, so it can be extended with arbitrary JSON properties to allow adding custom data.
  /// </summary>
  public object this[string k] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}

public class GetBaseReportRequest
{
  [JsonProperty("customData", NullValueHandling = NullValueHandling.Ignore)]
  CustomDataType? CustomData { get; set; }

  [JsonProperty("reportBase", NullValueHandling = NullValueHandling.Ignore)]
  ReportBaseEnumType ReportBase { get; set; }

  /// <summary>
  /// The Id of the request.
  /// </summary>
  [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
  double RequestId { get; set; }
}

/// <summary>
/// This field specifies the report base.
/// </summary>
[JsonConverter(typeof(ReportBaseEnumTypeNewtonsoftJsonConverter))]
public enum ReportBaseEnumType
{
  ///<summary>
  /// ConfigurationInventory
  ///</summary>
  ConfigurationInventory,
  ///<summary>
  /// FullInventory
  ///</summary>
  FullInventory,
  ///<summary>
  /// SummaryInventory
  ///</summary>
  SummaryInventory
}

class ReportBaseEnumTypeNewtonsoftJsonConverter : JsonConverter
{
  public override bool CanConvert(Type t) => t == typeof(ReportBaseEnumType) || t == typeof(ReportBaseEnumType?);

  public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
      => reader.TokenType switch
      {
        JsonToken.String =>
                serializer.Deserialize<string>(reader) switch
                {
                  "ConfigurationInventory" => ReportBaseEnumType.ConfigurationInventory,
                  "FullInventory" => ReportBaseEnumType.FullInventory,
                  "SummaryInventory" => ReportBaseEnumType.SummaryInventory,
                  _ => throw new NotSupportedException("Cannot unmarshal type ReportBaseEnumType")
                },
        _ => throw new NotSupportedException("Cannot unmarshal type ReportBaseEnumType")
      };

  public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
  {
    if (untypedValue is null) { serializer.Serialize(writer, null); return; }
    var value = (ReportBaseEnumType)untypedValue;
    switch (value)
    {
      case ReportBaseEnumType.ConfigurationInventory: serializer.Serialize(writer, "ConfigurationInventory"); return;
      case ReportBaseEnumType.FullInventory: serializer.Serialize(writer, "FullInventory"); return;
      case ReportBaseEnumType.SummaryInventory: serializer.Serialize(writer, "SummaryInventory"); return;
      default: break;
    }
    throw new NotSupportedException("Cannot marshal type ReportBaseEnumType");
  }
}