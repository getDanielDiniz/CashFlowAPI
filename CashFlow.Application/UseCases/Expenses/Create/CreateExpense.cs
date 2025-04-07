using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.BaseExceptions;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Expenses.Create;
public class CreateExpense : ICreateExpense
{
    private readonly IWriteAndDeleteExpenseRepository _repository;
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public CreateExpense(IWriteAndDeleteExpenseRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unityOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        Expense expense = _mapper.Map<Expense>(request);

        await _repository.Add(expense);
        await _unityOfWork.Commit();

        ResponseExpenseJson response = _mapper.Map<ResponseExpenseJson>(expense);
        return response;
    }

    private static void Validate(RequestExpenseJson request)
    {
        ExpenseValidator validator = new();
        ValidationResult result = validator.Validate(request);

        if (result.IsValid == false)
        {
            List<string> errors = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnExpenseValidation(errors);
        }
    }


}
