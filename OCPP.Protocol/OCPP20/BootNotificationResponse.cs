/*
 * OCPP.Core - https://github.com/dallmann-consulting/OCPP.Core
 * Copyright (C) 2020-2021 dallmann consulting GmbH.
 * All Rights Reserved.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace OCPP.Protocol.OCPP20;

#pragma warning disable // Disable all warnings

/// <summary>This contains whether the Charging Station has been registered
/// within the CSMS.
/// </summary>
[GeneratedCode("NJsonSchema", "10.3.1.0 (Newtonsoft.Json v9.0.0.0)")]
public enum RegistrationStatusEnumType
{
  [EnumMember(Value = @"Accepted")]
  Accepted = 0,

  [EnumMember(Value = @"Pending")]
  Pending = 1,

  [EnumMember(Value = @"Rejected")]
  Rejected = 2,

}

/// <summary>Element providing more information about the status.
/// </summary>
[GeneratedCode("NJsonSchema", "10.3.1.0 (Newtonsoft.Json v9.0.0.0)")]
public partial class StatusInfoType
{
  [JsonProperty("customData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
  public CustomDataType CustomData { get; set; }

  /// <summary>A predefined code for the reason why the status is returned in this response. The string is case-insensitive.
  /// </summary>
  [JsonProperty("reasonCode", Required = Required.Always)]
  [Required(AllowEmptyStrings = true)]
  [StringLength(20)]
  public string ReasonCode { get; set; }

  /// <summary>Additional text to provide detailed information.
  /// </summary>
  [JsonProperty("additionalInfo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
  [StringLength(512)]
  public string AdditionalInfo { get; set; }


}

[GeneratedCode("NJsonSchema", "10.3.1.0 (Newtonsoft.Json v9.0.0.0)")]
public partial class BootNotificationResponse
{
  [JsonProperty("customData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
  public CustomDataType CustomData { get; set; }

  /// <summary>This contains the CSMS’s current time.
  /// </summary>
  [JsonProperty("currentTime", Required = Required.Always)]
  [Required(AllowEmptyStrings = true)]
  public DateTimeOffset CurrentTime { get; set; }

  /// <summary>When &amp;lt;&amp;lt;cmn_registrationstatusenumtype,Status&amp;gt;&amp;gt; is Accepted, this contains the heartbeat interval in seconds. If the CSMS returns something other than Accepted, the value of the interval field indicates the minimum wait time before sending a next BootNotification request.
  /// </summary>
  [JsonProperty("interval", Required = Required.Always)]
  public int Interval { get; set; }

  [JsonProperty("status", Required = Required.Always)]
  [Required(AllowEmptyStrings = true)]
  [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
  public RegistrationStatusEnumType Status { get; set; }

  [JsonProperty("statusInfo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
  public StatusInfoType StatusInfo { get; set; }


}