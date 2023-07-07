/*
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

namespace OCPP.Core.Server.Messages_OCPP20;

/// <summary>
/// Defined OCPP error codes
/// </summary>
public static class ErrorCodes
{
  /// <summary>
  /// Requested Action is recognized but not supported by the receiver
  /// </summary>
  public static readonly string NotSupported = "NotSupported";

  /// <summary>
  /// InternalError An internal error occurred and the receiver was not able to process the requested Action successfully
  /// </summary>
  public static readonly string InternalError = "InternalError";

  /// <summary>
  /// Payload for Action is incomplete
  /// </summary>
  public static readonly string ProtocolError = "ProtocolError";

  /// <summary>
  /// During the processing of Action a security issue occurred preventing receiver from completing the Action successfully
  /// </summary>
  public static readonly string SecurityError = "SecurityError";

  /// <summary>
  /// Payload for Action is syntactically incorrect or not conform the PDU structure for Action
  /// </summary>
  public static readonly string FormationViolation = "FormationViolation";

  /// <summary>
  /// Payload is syntactically correct but at least one field contains an invalid value
  /// </summary>
  public static readonly string PropertyConstraintViolation = "PropertyConstraintViolation";

  /// <summary>
  /// Payload for Action is syntactically correct but at least one of the fields violates occurrence constraints
  /// </summary>
  public static readonly string OccurrenceConstraintViolation = "OccurrenceConstraintViolation";

  /// <summary>
  ///  Payload for Action is syntactically correct but at least one of the fields violates data type constraints(e.g. “somereadonly string”: 12)
  /// </summary>
  public static readonly string TypeConstraintViolation = "TypeConstraintViolation";

  /// <summary>
  /// Any other error not covered by the previous ones
  /// </summary>
  public static readonly string GenericError = "GenericError";
}
