using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.Read.ReadAll;
using CashFlow.Application.UseCases.Expenses.Read.ReadById;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.PDF;
using CashFlow.Application.UseCases.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUsecases(services);
        AddAutoMapper(services);
    }

    private static void AddUsecases(IServiceCollection services)
    {
        services.AddScoped<ICreateExpense, CreateExpense>();
        services.AddScoped<IReadAllExpenses, ReadAllExpenses>();
        services.AddScoped<IReadById, ReadById>();
        services.AddScoped<IDeleteExpense, DeleteExpense>();
        services.AddScoped<IUpdateExpense, UpdateExpense>();
        services.AddScoped<IExpenseExcelReport, ExpenseExcelReport>();
        services.AddScoped<IExpensePDFReport, ExpensePDFReport>();
    }
    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
