using Newtonsoft.Json;
using OCPP.Core.Domain.Entities;
using OCPP.Protocol.OCPP20;

namespace OCPP.Core.WebApi.Services.OCPP;

public partial class OCPPService
{
  public string HandleBootNotification(OCPPMessage msgIn, OCPPMessage msgOut)
  {
    string errorCode = string.Empty;
    string bootReason = string.Empty;
    try
    {
      Console.WriteLine("Processing boot notification...");
      BootNotificationRequest? bootNotificationRequest = JsonConvert.DeserializeObject<BootNotificationRequest>(msgIn.JsonPayload);
      Console.WriteLine("BootNotification => Message deserialized");

      bootReason = bootNotificationRequest?.Reason.ToString() ?? string.Empty;
      Console.WriteLine("BootNotification => Reason={0}", bootReason);

      BootNotificationResponse bootNotificationResponse = new()
      {
        CurrentTime = DateTimeOffset.UtcNow,
        Interval = 10,    // 10 seconds
        StatusInfo = new StatusInfoType
        {
          ReasonCode = string.Empty,
          AdditionalInfo = string.Empty
        },
        CustomData = new CustomDataType
        {
          VendorId = VendorId
        }
      };

      if (ChargePointStatus != null)
      {
        // Known charge station => accept
        bootNotificationResponse.Status = RegistrationStatusEnumType.Accepted;
      }
      else
      {
        // Unknown charge station => reject
        bootNotificationResponse.Status = RegistrationStatusEnumType.Rejected;
      }

      msgOut.JsonPayload = JsonConvert.SerializeObject(bootNotificationResponse);
      Console.WriteLine("BootNotification => Response serialized");
    }
    catch (Exception exp)
    {
      Console.WriteLine("BootNotification => Exception: {0}", exp.Message);
      errorCode = ErrorCodes.FormationViolation;
    }

    _logService.WriteMessageLog(ChargePointStatus!.Id, null, msgIn.Action, bootReason, errorCode);
    return errorCode;
  }
}