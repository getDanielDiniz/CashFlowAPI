using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.BaseExceptions;
using CashFlow.Exception.Resource;
using Microsoft.Extensions.Options;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpense : IUpdateExpense
{
    private readonly IUpdateOnlyExpenseRepository _update;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public async Task Execute(long id, RequestExpenseJson request)
    {
        Expense? expenseToUpdate = await _update.GetById_Tracking(id);

        Validate(request);

        _mapper.Map( request, expenseToUpdate);

        if (expenseToUpdate == null) {
            throw new NotFoundError(ResourceExpense.EXPENSE_NOT_FOUND);
        }

        _update.UpdateExpense(expenseToUpdate);
        await _unitOfWork.Commit();
    }

    private static void Validate(RequestExpenseJson request)
    {
        ExpenseValidator validator = new();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            List<string> errors = result.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ErrorOnExpenseValidation(errors);
        }
    }


    public UpdateExpense(IUpdateOnlyExpenseRepository update, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _update = update;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
