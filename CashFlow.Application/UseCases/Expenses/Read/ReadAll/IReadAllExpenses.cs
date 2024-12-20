using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Read.ReadAll;
public interface IReadAllExpenses
{
    Task<ResponseAllExpensesJson> Execute();
}
