using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories;
internal class ExpenseRepository : IWriteAndDeleteExpenseRepository, IReadOnlyExpenseRepository, IUpdateOnlyExpenseRepository
{
    private readonly CashFlowDbContext _dbContext;
    public ExpenseRepository(CashFlowDbContext context)
    {
        _dbContext = context;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        Expense? result = await _dbContext.Expenses.FirstOrDefaultAsync(exp => exp.Id == id);

        if (result == null) {
            return false;
        }
        else{
            _dbContext.Expenses.Remove(result);
            return true;
        }
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IReadOnlyExpenseRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(exp => exp.Id == id);
    }

    async Task<Expense?> IUpdateOnlyExpenseRepository.GetById_Tracking(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(exp => exp.Id == id);
    }

    public void UpdateExpense(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly month)
    {
        DateTime startDate = new(year: month.Year, month: month.Month,day: 1);
        int lastDayInMonth = DateTime.DaysInMonth(month.Year, month.Month);
        DateTime endDate = new(year: month.Year,month: month.Month,day: lastDayInMonth, hour: 23,minute: 59, second:59);

        return await _dbContext.Expenses
            .AsNoTracking()
            .Where(exp =>exp.Date >= startDate && exp.Date <= endDate)
            .OrderBy(exp => exp.Date)
            .ToListAsync();
    }
}
