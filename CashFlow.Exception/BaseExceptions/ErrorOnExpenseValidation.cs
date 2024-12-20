using System.Net;

namespace CashFlow.Exception.BaseExceptions;
public class ErrorOnExpenseValidation : CashFlowBaseException
{
    public List<string> Errors { get; set; }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnExpenseValidation(string error)
    {
        Errors = [error];
    }

    public ErrorOnExpenseValidation(List<string> errorsMessages)
    {
        Errors = errorsMessages;
    }

    public override List<string> getErrors()
    {
        return Errors;
    }
}
