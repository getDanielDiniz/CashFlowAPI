using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Create;
public interface ICreateExpense
{
    Task<ResponseExpenseJson> Execute(RequestExpenseJson request);
}
