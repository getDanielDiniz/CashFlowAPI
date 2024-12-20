namespace CashFlow.Application.UseCases.Expenses.Reports.PDF;
public interface IExpensePDFReport
{
    Task<byte[]> Execute(DateOnly month);
}
