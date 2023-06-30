using System.Runtime.Serialization;

namespace OCPP.Core.Domain.Common.Enums;

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