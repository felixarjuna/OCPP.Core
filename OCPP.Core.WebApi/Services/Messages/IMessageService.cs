using System.Net.WebSockets;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.Messages;

public interface IMessageService
{
  Task ReceiveOCPP20(ChargePointStatus status);
  Task ResetOCPP20(ChargePointStatus status);
  Task UnlockConnectorOCPP20(ChargePointStatus status);
  Task SendMessageOCPP20(OCPPMessage message, WebSocket webSocket);

  Task ReceiveOCPP16(ChargePointStatus status);
  Task ResetOCPP16(ChargePointStatus status);
  Task UnlockConnectorOCPP16(ChargePointStatus status);
  Task SendMessageOCPP16(OCPPMessage message, WebSocket webSocket);
}