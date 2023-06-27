using ErrorOr;

namespace OCPP.Core.Domain.Common.Errors;

public static partial class Errors
{
  public class ChargePoint
  {
    public static Error NotFound => Error.NotFound(
      code: "ChargePoint:NotFound",
      description: "ChargePoint not found."
    );
  }
}