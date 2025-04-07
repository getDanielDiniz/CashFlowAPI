using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.Read.ReadAll;
public interface IReadAllExpenses
{
    Task<ResponseAllExpensesJson> Execute();
}
