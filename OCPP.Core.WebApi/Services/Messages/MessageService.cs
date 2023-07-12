using System.Net.WebSockets;
using OCPP.Core.Domain.Entities;

namespace OCPP.Core.WebApi.Services.Messages;

public class MessageService : IMessageService
{

  public Task ReceiveOCPP16(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task ResetOCPP16(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task UnlockConnectorOCPP16(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task SendMessageOCPP16(OCPPMessage message, WebSocket webSocket)
  {
    throw new NotImplementedException();
  }

  public Task ReceiveOCPP20(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task ResetOCPP20(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }

  public Task SendMessageOCPP20(OCPPMessage message, WebSocket webSocket)
  {
    throw new NotImplementedException();
  }

  public Task UnlockConnectorOCPP20(ChargePointStatus status)
  {
    throw new NotImplementedException();
  }
}