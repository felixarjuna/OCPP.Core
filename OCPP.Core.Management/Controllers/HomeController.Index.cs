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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OCPP.Core.Database;
using OCPP.Core.Management.Models;

namespace OCPP.Core.Management.Controllers;

public partial class HomeController : BaseController
{
  private readonly IStringLocalizer<HomeController> _localizer;

  public HomeController(
      UserManager userManager,
      IStringLocalizer<HomeController> localizer,
      ILoggerFactory loggerFactory,
      IConfiguration config) : base(userManager, loggerFactory, config)
  {
    _localizer = localizer;
    Logger = loggerFactory.CreateLogger<HomeController>();
  }

  [Authorize]
  public async Task<IActionResult> Index()
  {
    Logger.LogTrace("Index: Loading charge points with latest transactions...");

    OverviewViewModel overviewModel = new OverviewViewModel();
    overviewModel.ChargePoints = new List<ChargePointsOverviewViewModel>();
    try
    {
      Dictionary<string, ChargePointStatus> dictOnlineStatus = new Dictionary<string, ChargePointStatus>();
      #region Load online status from OCPP server
      string serverApiUrl = base.Config.GetValue<string>("ServerApiUrl");
      string apiKeyConfig = base.Config.GetValue<string>("ApiKey");
      if (!string.IsNullOrEmpty(serverApiUrl))
      {
        bool serverError = false;
        try
        {
          ChargePointStatus[] onlineStatusList = null;

          using (var httpClient = new HttpClient())
          {
            if (!serverApiUrl.EndsWith('/'))
            {
              serverApiUrl += "/";
            }
            Uri uri = new Uri(serverApiUrl);
            uri = new Uri(uri, "Status");
            httpClient.Timeout = new TimeSpan(0, 0, 4); // use short timeout

            // API-Key authentication?
            if (!string.IsNullOrWhiteSpace(apiKeyConfig))
            {
              httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKeyConfig);
            }
            else
            {
              Logger.LogWarning("Index: No API-Key configured!");
            }

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
              string jsonData = await response.Content.ReadAsStringAsync();
              if (!string.IsNullOrEmpty(jsonData))
              {
                onlineStatusList = JsonConvert.DeserializeObject<ChargePointStatus[]>(jsonData);
                overviewModel.ServerConnection = true;

                if (onlineStatusList != null)
                {
                  foreach (ChargePointStatus cps in onlineStatusList)
                  {
                    if (!dictOnlineStatus.TryAdd(cps.Id, cps))
                    {
                      Logger.LogError("Index: Online charge point status (ID={0}) could not be added to dictionary", cps.Id);
                    }
                  }
                }
              }
              else
              {
                Logger.LogError("Index: Result of status web request is empty");
                serverError = true;
              }
            }
            else
            {
              Logger.LogError("Index: Result of status web request => httpStatus={0}", response.StatusCode);
              serverError = true;
            }
          }

          Logger.LogInformation("Index: Result of status web request => Length={0}", onlineStatusList?.Length);
        }
        catch (Exception exp)
        {
          Logger.LogError(exp, "Index: Error in status web request => {0}", exp.Message);
          serverError = true;
        }

        if (serverError)
        {
          ViewBag.ErrorMsg = _localizer["ErrorOCPPServer"];
        }
      }
      #endregion

      using (OCPPCoreContext dbContext = new OCPPCoreContext(this.Config))
      {
        // List of charge point status (OCPP messages) with latest transaction (if one exist)
        List<ConnectorStatusView> connectorStatusViewList = dbContext.ConnectorStatusViews.ToList<ConnectorStatusView>();

        // Count connectors for every charge point (=> naming scheme)
        Dictionary<string, int> dictConnectorCount = new Dictionary<string, int>();
        foreach (ConnectorStatusView csv in connectorStatusViewList)
        {
          if (dictConnectorCount.ContainsKey(csv.ChargePointId))
          {
            // > 1 connector
            dictConnectorCount[csv.ChargePointId] = dictConnectorCount[csv.ChargePointId] + 1;
          }
          else
          {
            // first connector
            dictConnectorCount.Add(csv.ChargePointId, 1);
          }
        }

        // List of configured charge points
        List<ChargeStation> dbChargePoints = dbContext.ChargeStations.ToList<ChargeStation>();
        if (dbChargePoints != null)
        {
          // Iterate through all charge points in database
          foreach (ChargeStation cp in dbChargePoints)
          {
            ChargePointStatus cpOnlineStatus = null;
            dictOnlineStatus.TryGetValue(cp.ChargeStationId, out cpOnlineStatus);

            // Preference: Check for connectors status in database
            bool foundConnectorStatus = false;
            if (connectorStatusViewList != null)
            {
              foreach (ConnectorStatusView connStatus in connectorStatusViewList)
              {
                if (string.Equals(cp.ChargeStationId, connStatus.ChargePointId, StringComparison.InvariantCultureIgnoreCase))
                {
                  foundConnectorStatus = true;

                  ChargePointsOverviewViewModel cpovm = new ChargePointsOverviewViewModel();
                  cpovm.ChargePointId = cp.ChargeStationId;
                  cpovm.ConnectorId = connStatus.ConnectorId;
                  if (string.IsNullOrWhiteSpace(connStatus.ConnectorName))
                  {
                    // No connector name specified => use default
                    if (dictConnectorCount.ContainsKey(cp.ChargeStationId) &&
                        dictConnectorCount[cp.ChargeStationId] > 1)
                    {
                      // more than 1 connector => "<charge point name>:<connector no.>"
                      cpovm.Name = $"{cp.Name}:{connStatus.ConnectorId}";
                    }
                    else
                    {
                      // only 1 connector => "<charge point name>"
                      cpovm.Name = cp.Name;
                    }
                  }
                  else
                  {
                    // Connector has name override name specified
                    cpovm.Name = connStatus.ConnectorName;
                  }
                  cpovm.Online = cpOnlineStatus != null;
                  cpovm.ConnectorStatus = ConnectorStatusEnum.Undefined;
                  OnlineConnectorStatus onlineConnectorStatus = null;
                  if (cpOnlineStatus != null &&
                      cpOnlineStatus.OnlineConnectors != null &&
                      cpOnlineStatus.OnlineConnectors.ContainsKey(connStatus.ConnectorId))
                  {
                    onlineConnectorStatus = cpOnlineStatus.OnlineConnectors[connStatus.ConnectorId];
                    cpovm.ConnectorStatus = onlineConnectorStatus.Status;
                    Logger.LogTrace("Index: Found online status for CP='{0}' / Connector='{1}' / Status='{2}'", cpovm.ChargePointId, cpovm.ConnectorId, cpovm.ConnectorStatus);
                  }

                  if (connStatus.TransactionId.HasValue)
                  {
                    cpovm.MeterStart = connStatus.MeterStart.Value;
                    cpovm.MeterStop = connStatus.MeterStop;
                    cpovm.StartTime = connStatus.StartTime;
                    cpovm.StopTime = connStatus.StopTime;

                    // default status: active transaction or not?
                    cpovm.ConnectorStatus = (cpovm.StopTime.HasValue) ? ConnectorStatusEnum.Available : ConnectorStatusEnum.Occupied;
                  }
                  else
                  {
                    cpovm.MeterStart = -1;
                    cpovm.MeterStop = -1;
                    cpovm.StartTime = null;
                    cpovm.StopTime = null;

                    // default status: Available
                    cpovm.ConnectorStatus = ConnectorStatusEnum.Available;
                  }

                  // Add current charge data to view model
                  if (cpovm.ConnectorStatus == ConnectorStatusEnum.Occupied &&
                      onlineConnectorStatus != null)
                  {
                    string currentCharge = string.Empty;
                    if (onlineConnectorStatus.ChargeRateKW != null)
                    {
                      currentCharge = string.Format("{0:0.0}kW", onlineConnectorStatus.ChargeRateKW.Value);
                    }
                    if (onlineConnectorStatus.SoC != null)
                    {
                      if (!string.IsNullOrWhiteSpace(currentCharge)) currentCharge += " | ";
                      currentCharge += string.Format("{0:0}%", onlineConnectorStatus.SoC.Value);
                    }
                    if (!string.IsNullOrWhiteSpace(currentCharge))
                    {
                      cpovm.CurrentChargeData = currentCharge;
                    }
                  }

                  overviewModel.ChargePoints.Add(cpovm);
                }
              }
            }
            // Fallback: assume 1 connector and show main charge point
            if (foundConnectorStatus == false)
            {
              // no connector status found in DB => show configured charge point in overview
              ChargePointsOverviewViewModel cpovm = new ChargePointsOverviewViewModel();
              cpovm.ChargePointId = cp.ChargeStationId;
              cpovm.ConnectorId = 0;
              cpovm.Name = cp.Name;
              cpovm.Comment = cp.Comment;
              cpovm.Online = cpOnlineStatus != null;
              cpovm.ConnectorStatus = ConnectorStatusEnum.Undefined;
              overviewModel.ChargePoints.Add(cpovm);
            }
          }
        }

        Logger.LogInformation("Index: Found {0} charge points / connectors", overviewModel.ChargePoints?.Count);
      }
    }
    catch (Exception exp)
    {
      Logger.LogError(exp, "Index: Error loading charge points from database");
      TempData["ErrMessage"] = exp.Message;
      return RedirectToAction("Error", new { Id = "" });
    }

    return View(overviewModel);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View();
  }
}
