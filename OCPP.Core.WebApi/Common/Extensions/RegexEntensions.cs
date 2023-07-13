using System.Text.RegularExpressions;

namespace OCPP.Core.WebApi.Common.Extensions;

public static class RegexExtensions
{
  public static void Deconstruct(
    this GroupCollection groups,
    out string messageTypeId,
    out string uniqueId,
    out string action,
    out string jsonPayload)
  {
    messageTypeId = groups[1].Value;
    uniqueId = groups[2].Value;
    action = groups[3].Value;
    jsonPayload = groups[4].Value;
  }
}