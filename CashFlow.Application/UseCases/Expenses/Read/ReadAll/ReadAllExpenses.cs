using AutoMapper;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Read.ReadAll;
public class ReadAllExpenses : IReadAllExpenses
{
    private IReadOnlyExpenseRepository _repository;
    private IMapper _mapper;
    public async Task<ResponseAllExpensesJson> Execute()
    {
        var expensesList =  await _repository.GetAll();
        return new ResponseAllExpensesJson {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(expensesList)
        };



    }
    public ReadAllExpenses(IReadOnlyExpenseRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
}
