
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.BaseExceptions;
using CashFlow.Exception.Resource;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpense : IDeleteExpense
{
    private readonly IWriteAndDeleteExpenseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public async Task Execute(long id)
    {
        bool isExpenseDeleted = await _repository.Delete(id);
        
        if (isExpenseDeleted is not true) {
            throw new NotFoundError(ResourceExpense.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }

    public DeleteExpense(IWriteAndDeleteExpenseRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
}
