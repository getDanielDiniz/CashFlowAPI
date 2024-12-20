using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IWriteAndDeleteExpenseRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// Return TRUE if the expense was deleted.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
    
}
