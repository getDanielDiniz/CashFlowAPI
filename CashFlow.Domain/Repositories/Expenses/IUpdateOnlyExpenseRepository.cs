using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IUpdateOnlyExpenseRepository
{  
    Task<Expense?> GetById_Tracking(long id);
    void UpdateExpense(Expense expense);
}

