using OCPP.Core.Domain.Entities;
using OCPP.Core.WebApi.Persistence;

namespace OCPP.Core.WebApi.Services.Log;

public class LogService : ILogService
{
  private readonly OCPPCoreDbContext _context;

  public LogService(OCPPCoreDbContext context)
  {
    _context = context;
  }

  public void WriteMessageLog(
    string chargePointId,
    int? connectorId,
    string message,
    string? result,
    string errorCode)
  {
    MessageLog messageLog = MessageLog.Create(DateTime.UtcNow, chargePointId, connectorId, message, result, errorCode);
    _context.MessageLogs.Add(messageLog);
    _context.SaveChanges();
  }
}