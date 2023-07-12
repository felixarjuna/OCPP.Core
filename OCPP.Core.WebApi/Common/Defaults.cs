namespace OCPP.Core.WebApi.Common;

public static class Defaults
{
  public const string Protocol_OCPP16 = "ocpp1.6";
  public const string Protocol_OCPP20 = "ocpp2.0";

  public static readonly string[] SupportedProtocols = { Protocol_OCPP20, Protocol_OCPP16 };
}