namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public interface IExpenseExcelReport
{
    Task<byte[]> Execute(DateOnly month);
}
