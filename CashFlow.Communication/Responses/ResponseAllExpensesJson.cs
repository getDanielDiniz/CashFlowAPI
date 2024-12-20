namespace CashFlow.Communication.Responses;
public class ResponseAllExpensesJson
{
    public required List<ResponseShortExpenseJson> Expenses { get; set; }
}
