using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infraestructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infraestructure.DataAccess;
public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IWriteAndDeleteExpenseRepository, ExpenseRepository>();
        services.AddScoped<IReadOnlyExpenseRepository, ExpenseRepository>();
        services.AddScoped<IUpdateOnlyExpenseRepository, ExpenseRepository>();
        services.AddScoped<IUnitOfWork, UnityOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MySqlConnection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<CashFlowDbContext>(confg => confg.UseMySql(connectionString,serverVersion) );
    }
}
