namespace OCPP.Core.WebApi.Services.OCPP;

public class OCPPBaseService
{
  protected bool UpdateConnectorStatus(
    int connectorId,
    string status,
    DateTimeOffset? statusTime,
    double? meter,
    DateTimeOffset? meterTime)
  {
    return false;
  }

  protected string CleanChargeTagId(string rawChargeTagId)
  {
    throw new NotImplementedException();
  }
  protected DateTimeOffset? MaxExpiryDate()
  {
    throw new NotImplementedException();
  }
}