using ErrorOr;

namespace OCPP.Core.Domain.Common.Errors;

public static class Errors
{
  public static class ChargePoint
  {
    public static Error NotFound => Error.NotFound(
      code: "ChargePoint:NotFound",
      description: "ChargePoint not found."
    );

    public static Error AlreadyExists => Error.Conflict(
      code: "ChargePoint:AlreadyExists",
      description: "ChargePoint with given Id already exists.");
  }
}