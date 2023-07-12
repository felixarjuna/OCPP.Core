using System.Text.RegularExpressions;

namespace OCPP.Core.WebApi.Common;

public static partial class DefaultRegex
{
  public static readonly Regex OCPPMessage = OCPPMessageRegexPattern();

  [GeneratedRegex("^\\[\\s*(\\d)\\s*,\\s*\"([^\"]*)\"\\s*,(?:\\s*\"(\\w*)\"\\s*,)?\\s*(.*)\\s*\\]$")]
  private static partial Regex OCPPMessageRegexPattern();
}