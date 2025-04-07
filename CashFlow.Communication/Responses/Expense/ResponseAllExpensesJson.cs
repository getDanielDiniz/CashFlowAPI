namespace CashFlow.Communication.Responses.Expense;
public class ResponseAllExpensesJson
{
    public required List<ResponseShortExpenseJson> Expenses { get; set; }
}
