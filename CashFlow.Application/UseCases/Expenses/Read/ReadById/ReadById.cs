using AutoMapper;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.BaseExceptions;
using CashFlow.Exception.Resource;

namespace CashFlow.Application.UseCases.Expenses.Read.ReadById;
public class ReadById : IReadById
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyExpenseRepository _repository;

    public ReadById(IMapper mapper,
        IReadOnlyExpenseRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var expense = await _repository.GetById(id) ?? 
            throw new NotFoundError(ResourceExpense.EXPENSE_NOT_FOUND);

        ResponseExpenseJson response = _mapper.Map<ResponseExpenseJson>(expense);
        return response;
    }
}
