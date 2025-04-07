
using System.Net;

namespace CashFlow.Exception.BaseExceptions;

public class ErrorOnUserValidation : CashFlowBaseException
{
    public List<string> Errors { get; set; }
    public ErrorOnUserValidation(List<string> errors)
    {
        Errors = errors;
    }
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> getErrors()
    {
        return Errors;
    }
}
