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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OCPP.Core.Server.Messages_OCPP20
{
#pragma warning disable // Disable all warnings

  /// <summary>Used algorithms for the hashes provided.
  /// </summary>
  [GeneratedCode("NJsonSchema", "10.3.1.0 (Newtonsoft.Json v9.0.0.0)")]
  public enum HashAlgorithmEnumType
  {
    [EnumMember(Value = @"SHA256")]
    SHA256 = 0,

    [EnumMember(Value = @"SHA384")]
    SHA384 = 1,

    [EnumMember(Value = @"SHA512")]
    SHA512 = 2,

  }

  [GeneratedCode("NJsonSchema", "10.3.1.0 (Newtonsoft.Json v9.0.0.0)")]
  public partial class OCSPRequestDataType
  {
    [JsonProperty("customData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public CustomDataType CustomData { get; set; }

    [JsonProperty("hashAlgorithm", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [JsonConverter(typeof(StringEnumConverter))]
    public HashAlgorithmEnumType HashAlgorithm { get; set; }

    /// <summary>Hashed value of the Issuer DN (Distinguished Name).
    /// 
    /// </summary>
    [JsonProperty("issuerNameHash", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [StringLength(128)]
    public string IssuerNameHash { get; set; }

    /// <summary>Hashed value of the issuers public key
    /// </summary>
    [JsonProperty("issuerKeyHash", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [StringLength(128)]
    public string IssuerKeyHash { get; set; }

    /// <summary>The serial number of the certificate.
    /// </summary>
    [JsonProperty("serialNumber", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [StringLength(40)]
    public string SerialNumber { get; set; }

    /// <summary>This contains the responder URL (Case insensitive). 
    /// 
    /// </summary>
    [JsonProperty("responderURL", Required = Required.Always)]
    [Required(AllowEmptyStrings = true)]
    [StringLength(512)]
    public string ResponderURL { get; set; }
  }

  [GeneratedCode("NJsonSchema", "10.3.1.0 (Newtonsoft.Json v9.0.0.0)")]
  public partial class AuthorizeRequest
  {
    [JsonProperty("customData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public CustomDataType CustomData { get; set; }

    [JsonProperty("idToken", Required = Required.Always)]
    [Required]
    public IdTokenType IdToken { get; set; } = new IdTokenType();

    /// <summary>The X.509 certificated presented by EV and encoded in PEM format.
    /// </summary>
    [JsonProperty("certificate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    [StringLength(5500)]
    public string Certificate { get; set; }

    [JsonProperty("iso15118CertificateHashData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    [MinLength(1)]
    [MaxLength(4)]
    public ICollection<OCSPRequestDataType> Iso15118CertificateHashData { get; set; }
  }
}
