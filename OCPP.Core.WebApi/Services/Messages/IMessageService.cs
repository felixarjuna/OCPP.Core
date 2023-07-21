using System.Net.WebSockets;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.Messages;

public interface IMessageService
{
  Task ReceiveOCPP20(ChargeStationStatus status);
  Task ResetOCPP20(ChargeStationStatus status);
  Task UnlockConnectorOCPP20(ChargeStationStatus status);
  Task SendMessageOCPP20(OCPPMessage message, WebSocket webSocket);

  Task ReceiveOCPP16(ChargeStationStatus status);
  Task ResetOCPP16(ChargeStationStatus status);
  Task UnlockConnectorOCPP16(ChargeStationStatus status);
  Task SendMessageOCPP16(OCPPMessage message, WebSocket webSocket);
}