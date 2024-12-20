using CashFlow.Domain.Types.Payment;

namespace CashFlow.Domain.Entities;
public class Expense
{

    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTime Date {  get; set; }
}
