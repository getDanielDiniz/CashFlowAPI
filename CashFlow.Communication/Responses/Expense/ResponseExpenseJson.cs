using CashFlow.Communication.Types.PaymentType;

namespace CashFlow.Communication.Responses.Expense;
public class ResponseExpenseJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
