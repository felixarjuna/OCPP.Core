using Newtonsoft.Json;
using OCPP.Core.Domain.Common.Enums;
using OCPP.Core.Domain.Entities;
using OCPP.Protocol.OCPP20;

namespace OCPP.Core.WebApi.Services.OCPP;

public partial class OCPPService
{
  public string HandleStatusNotification(OCPPMessage msgIn, OCPPMessage msgOut)
  {
    if (ChargePointStatus is null) return ErrorCodes.InternalError;
    string errorCode = string.Empty;
    StatusNotificationResponse statusNotificationResponse = new()
    {
      CustomData = new CustomDataType
      {
        VendorId = VendorId
      }
    };

    int connectorId = 0;
    try
    {
      Console.WriteLine("Processing status notification...");
      StatusNotificationRequest? statusNotificationRequest = JsonConvert.DeserializeObject<StatusNotificationRequest>(msgIn.JsonPayload);
      if (statusNotificationRequest is null) return ErrorCodes.FormationViolation;
      Console.WriteLine("StatusNotification => Message deserialized");

      connectorId = statusNotificationRequest.ConnectorId;

      // Write raw status in DB
      _logService.WriteMessageLog(ChargePointStatus.Id, connectorId, msgIn.Action, string.Format("Status={0}", statusNotificationRequest.ConnectorStatus), string.Empty);

      ConnectorStatusEnum newStatus = ConnectorStatusEnum.Undefined;

      switch (statusNotificationRequest.ConnectorStatus)
      {
        case ConnectorStatusEnumType.Available:
          newStatus = ConnectorStatusEnum.Available;
          break;
        case ConnectorStatusEnumType.Occupied:
        case ConnectorStatusEnumType.Reserved:
          newStatus = ConnectorStatusEnum.Occupied;
          break;
        case ConnectorStatusEnumType.Unavailable:
          newStatus = ConnectorStatusEnum.Unavailable;
          break;
        case ConnectorStatusEnumType.Faulted:
          newStatus = ConnectorStatusEnum.Faulted;
          break;
      }
      Console.WriteLine("StatusNotification => ChargePoint={0} / Connector={1} / newStatus={2}", ChargePointStatus.Id, connectorId, newStatus.ToString());

      if (connectorId > 0)
      {
        if (!UpdateConnectorStatus(connectorId, newStatus.ToString(), statusNotificationRequest.Timestamp, null, null))
        {
          errorCode = ErrorCodes.InternalError;
        }

        if (ChargePointStatus.OnlineConnectors!.ContainsKey(connectorId))
        {
          OnlineConnectorStatus ocs = ChargePointStatus.OnlineConnectors[connectorId];
          ocs.Status = newStatus;
        }
        else
        {
          OnlineConnectorStatus ocs = new();
          ocs.Status = newStatus;
          if (ChargePointStatus.OnlineConnectors.TryAdd(connectorId, ocs))
          {
            Console.WriteLine("StatusNotification => new OnlineConnectorStatus with values: ChargePoint={0} / Connector={1} / newStatus={2}", ChargePointStatus.Id, connectorId, newStatus.ToString());
          }
          else
          {
            Console.WriteLine("StatusNotification => Error adding new OnlineConnectorStatus for ChargePoint={0} / Connector={1}", ChargePointStatus.Id, connectorId);
          }
        }
      }
      else
      {
        Console.WriteLine("StatusNotification => Status for unexpected ConnectorId={1} on ChargePoint={0}", ChargePointStatus?.Id, connectorId);
      }

      msgOut.JsonPayload = JsonConvert.SerializeObject(statusNotificationResponse);
      Console.WriteLine("StatusNotification => Response serialized");
    }
    catch (Exception exp)
    {
      Console.WriteLine("StatusNotification => ChargePoint={0} / Exception: {1}", ChargePointStatus.Id, exp.Message);
      errorCode = ErrorCodes.InternalError;
      _logService.WriteMessageLog(ChargePointStatus!.Id, connectorId, msgIn.Action, null, errorCode);
    }

    return errorCode;
  }
}