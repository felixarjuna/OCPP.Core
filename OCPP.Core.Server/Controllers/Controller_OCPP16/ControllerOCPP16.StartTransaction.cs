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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OCPP.Core.Database;
using OCPP.Core.Server.Messages_OCPP16;

namespace OCPP.Core.Server;

public partial class ControllerOCPP16
{
  public string HandleStartTransaction(OCPPMessage msgIn, OCPPMessage msgOut)
  {
    string errorCode = null;
    StartTransactionResponse startTransactionResponse = new StartTransactionResponse();

    int connectorId = -1;

    try
    {
      Logger.LogTrace("Processing startTransaction request...");
      StartTransactionRequest startTransactionRequest = JsonConvert.DeserializeObject<StartTransactionRequest>(msgIn.JsonPayload);
      Logger.LogTrace("StartTransaction => Message deserialized");

      string idTag = CleanChargeTagId(startTransactionRequest.IdTag, Logger);
      connectorId = startTransactionRequest.ConnectorId;

      startTransactionResponse.IdTagInfo.ParentIdTag = string.Empty;
      startTransactionResponse.IdTagInfo.ExpiryDate = MaxExpiryDate;

      if (string.IsNullOrWhiteSpace(idTag))
      {
        // no RFID-Tag => accept request
        startTransactionResponse.IdTagInfo.Status = IdTagInfoStatus.Accepted;
        Logger.LogInformation("StartTransaction => no charge tag => Status: {0}", startTransactionResponse.IdTagInfo.Status);
      }
      else
      {
        try
        {
          using (OCPPCoreContext dbContext = new OCPPCoreContext(Configuration))
          {
            ChargeTag ct = dbContext.Find<ChargeTag>(idTag);
            if (ct != null)
            {
              if (ct.ExpiryDate.HasValue) startTransactionResponse.IdTagInfo.ExpiryDate = ct.ExpiryDate.Value;
              startTransactionResponse.IdTagInfo.ParentIdTag = ct.ParentTagId;
              if (ct.Blocked == true)
              {
                startTransactionResponse.IdTagInfo.Status = IdTagInfoStatus.Blocked;
              }
              else if (ct.ExpiryDate < DateTime.Now)
              {
                startTransactionResponse.IdTagInfo.Status = IdTagInfoStatus.Expired;
              }
              else
              {
                startTransactionResponse.IdTagInfo.Status = IdTagInfoStatus.Accepted;
              }
            }
            else
            {
              startTransactionResponse.IdTagInfo.Status = IdTagInfoStatus.Invalid;
            }

            Logger.LogInformation("StartTransaction => Charge tag='{0}' => Status: {1}", idTag, startTransactionResponse.IdTagInfo.Status);
          }
        }
        catch (Exception exp)
        {
          Logger.LogError(exp, "StartTransaction => Exception reading charge tag ({0}): {1}", idTag, exp.Message);
          startTransactionResponse.IdTagInfo.Status = IdTagInfoStatus.Invalid;
        }
      }

      if (connectorId > 0)
      {
        // Update meter value in db connector status 
        UpdateConnectorStatus(connectorId, ConnectorStatusEnum.Occupied.ToString(), startTransactionRequest.Timestamp, (double)startTransactionRequest.MeterStart / 1000, startTransactionRequest.Timestamp);
      }

      if (startTransactionResponse.IdTagInfo.Status == IdTagInfoStatus.Accepted)
      {
        try
        {
          using (OCPPCoreContext dbContext = new(Configuration))
          {
            Transaction transaction = new()
            {
              ChargePointId = ChargePointStatus?.Id,
              ConnectorId = startTransactionRequest.ConnectorId,
              StartTagId = idTag,
              StartTime = startTransactionRequest.Timestamp.UtcDateTime,
              MeterStart = (double)startTransactionRequest.MeterStart / 1000, // Meter value here is always Wh
              StartResult = startTransactionResponse.IdTagInfo.Status.ToString()
            };
            dbContext.Add<Transaction>(transaction);
            dbContext.SaveChanges();

            // Return DB-ID as transaction ID
            startTransactionResponse.TransactionId = transaction.TransactionId;
          }
        }
        catch (Exception exp)
        {
          Logger.LogError(exp, "StartTransaction => Exception writing transaction: chargepoint={0} / tag={1}", ChargePointStatus?.Id, idTag);
          errorCode = ErrorCodes.InternalError;
        }
      }

      msgOut.JsonPayload = JsonConvert.SerializeObject(startTransactionResponse);
      Logger.LogTrace("StartTransaction => Response serialized");
    }
    catch (Exception exp)
    {
      Logger.LogError(exp, "StartTransaction => Exception: {0}", exp.Message);
      errorCode = ErrorCodes.FormationViolation;
    }

    WriteMessageLog(ChargePointStatus?.Id, connectorId, msgIn.Action, startTransactionResponse.IdTagInfo?.Status.ToString(), errorCode);
    return errorCode;
  }
}
