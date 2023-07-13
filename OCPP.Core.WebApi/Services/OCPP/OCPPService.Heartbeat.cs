using Newtonsoft.Json;
using OCPP.Core.Domain.Entities;
using OCPP.Protocol.OCPP20;

namespace OCPP.Core.WebApi.Services.OCPP;

public partial class OCPPService
{
  public string HandleHeartBeat(OCPPMessage msgIn, OCPPMessage msgOut)
  {
    string errorCode = string.Empty;

    Console.WriteLine("Processing heartbeat...");
    HeartbeatResponse heartbeatResponse = new()
    {
      CurrentTime = DateTimeOffset.UtcNow,
      CustomData = new CustomDataType
      {
        VendorId = VendorId
      }
    };

    msgOut.JsonPayload = JsonConvert.SerializeObject(heartbeatResponse);
    Console.WriteLine("Heartbeat => Response serialized");

    _logService.WriteMessageLog(ChargePointStatus!.Id, null, msgIn.Action, null, errorCode);
    return errorCode;
  }
}