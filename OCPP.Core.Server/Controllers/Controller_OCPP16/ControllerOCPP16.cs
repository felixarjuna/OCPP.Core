﻿/*
 * OCPP.Core - https://github.com/dallmann-consulting/OCPP.Core
 * Copyright (C) 2020-2021 dallmann consulting GmbH.
 * All Rights Reserved.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OCPP.Core.Database;
using OCPP.Core.Server.Messages_OCPP16;

namespace OCPP.Core.Server;

public partial class ControllerOCPP16 : ControllerBase
{
  /// <summary>
  /// Constructor
  /// </summary>
  public ControllerOCPP16(IConfiguration config, ILoggerFactory loggerFactory, ChargePointStatus chargePointStatus) :
      base(config, loggerFactory, chargePointStatus)
  {
    Logger = loggerFactory.CreateLogger(typeof(ControllerOCPP16));
  }

  /// <summary>
  /// Processes the charge point message and returns the answer message
  /// </summary>
  public OCPPMessage ProcessRequest(OCPPMessage msgIn)
  {
    OCPPMessage msgOut = new()
    {
      MessageType = "3",
      UniqueId = msgIn.UniqueId
    };

    string errorCode;
    switch (msgIn.Action)
    {
      case "BootNotification":
        errorCode = HandleBootNotification(msgIn, msgOut);
        break;

      case "Heartbeat":
        errorCode = HandleHeartBeat(msgIn, msgOut);
        break;

      case "Authorize":
        errorCode = HandleAuthorize(msgIn, msgOut);
        break;

      case "StartTransaction":
        errorCode = HandleStartTransaction(msgIn, msgOut);
        break;

      case "StopTransaction":
        errorCode = HandleStopTransaction(msgIn, msgOut);
        break;

      case "MeterValues":
        errorCode = HandleMeterValues(msgIn, msgOut);
        break;

      case "StatusNotification":
        errorCode = HandleStatusNotification(msgIn, msgOut);
        break;

      case "DataTransfer":
        errorCode = HandleDataTransfer(msgIn, msgOut);
        break;

      default:
        errorCode = ErrorCodes.NotSupported;
        WriteMessageLog(ChargePointStatus.Id, null, msgIn.Action, msgIn.JsonPayload, errorCode);
        break;
    }

    if (!string.IsNullOrEmpty(errorCode))
    {
      // Invalid message type => return type "4" (CALLERROR)
      msgOut.MessageType = "4";
      msgOut.ErrorCode = errorCode;
      Logger.LogDebug("ControllerOCPP16 => Return error code messge: ErrorCode={0}", errorCode);
    }

    return msgOut;
  }

  /// <summary>
  /// Processes the charge point message and returns the answer message
  /// </summary>
  public void ProcessAnswer(OCPPMessage msgIn, OCPPMessage msgOut)
  {
    // The response (msgIn) has no action => check action in original request (msgOut)
    switch (msgOut.Action)
    {
      case "Reset":
        HandleReset(msgIn, msgOut);
        break;

      case "UnlockConnector":
        HandleUnlockConnector(msgIn, msgOut);
        break;

      default:
        WriteMessageLog(ChargePointStatus.Id, null, msgIn.Action, msgIn.JsonPayload, "Unknown answer");
        break;
    }
  }

  /// <summary>
  /// Helper function for writing a log entry in database
  /// </summary>
  private bool WriteMessageLog(string chargePointId, int? connectorId, string message, string result, string errorCode)
  {
    try
    {
      int dbMessageLog = Configuration.GetValue<int>("DbMessageLog", 0);
      if (dbMessageLog > 0 && !string.IsNullOrWhiteSpace(chargePointId))
      {
        bool doLog = (dbMessageLog > 1 ||
                        (message != "BootNotification" &&
                         message != "Heartbeat" &&
                         message != "DataTransfer" &&
                         message != "StatusNotification"));

        if (doLog)
        {
          using OCPPCoreContext dbContext = new OCPPCoreContext(Configuration);
          MessageLog msgLog = new()
          {
            ChargePointId = chargePointId,
            ConnectorId = connectorId,
            LogTime = DateTime.UtcNow,
            Message = message,
            Result = result,
            ErrorCode = errorCode
          };
          dbContext.MessageLogs.Add(msgLog);
          Logger.LogTrace("MessageLog => Writing entry '{0}'", message);
          dbContext.SaveChanges();
          return true;
        }
      }
    }
    catch (Exception exp)
    {
      Logger.LogError(exp, "MessageLog => Error writing entry '{0}'", message);
    }
    return false;
  }
}
