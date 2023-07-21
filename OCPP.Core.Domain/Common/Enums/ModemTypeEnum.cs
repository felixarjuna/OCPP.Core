using System.Runtime.Serialization;

namespace OCPP.Core.Domain.Common.Enums;

public enum ModemTypeEnum
{
  [EnumMember(Value = "iccid")]
  iccid = 0,

  [EnumMember(Value = "imsi")]
  imsi = 1,
}