namespace CashFlow.Exception.BaseExceptions;
public abstract class CashFlowBaseException : System.Exception
{
    protected CashFlowBaseException(string message) : base(message) { }
    protected CashFlowBaseException() {}

    public abstract List<string> getErrors();
    public abstract int StatusCode { get; }
}
