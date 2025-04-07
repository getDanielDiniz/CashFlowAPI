using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.Read.ReadById;
public interface IReadById
{
    Task<ResponseExpenseJson> Execute(long  id);
}
