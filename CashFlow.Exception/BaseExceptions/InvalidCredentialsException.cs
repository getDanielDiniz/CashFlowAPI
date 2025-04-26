
using System.Net;

namespace CashFlow.Exception.BaseExceptions;
public class InvalidCredentialsException : CashFlowBaseException
{
    public List<string> _Errors;
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public InvalidCredentialsException(string Error)
    {
        _Errors = [Error];
    }
    public override List<string> getErrors()
    {
        return _Errors;
    }
}
