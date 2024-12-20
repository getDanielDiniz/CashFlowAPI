
using System.Net;

namespace CashFlow.Exception.BaseExceptions;
public class NotFoundError : CashFlowBaseException
{
    public NotFoundError(string message) : base(message) {}

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> getErrors()
    {
        return [Message];
    }
}
