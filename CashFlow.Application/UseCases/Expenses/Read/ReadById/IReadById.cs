using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Read.ReadById;
public interface IReadById
{
    Task<ResponseExpenseJson> Execute(long  id);
}
