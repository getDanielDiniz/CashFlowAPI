using CashFlow.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infraestructure.Migrations;
public static class UpdateDatabaseByMigrations
{
    public static async Task UpdateDatabase(this IServiceProvider serviceProvider)
    {
        var DbContext = serviceProvider.GetRequiredService<CashFlowDbContext>();

        await DbContext.Database.MigrateAsync();
    }
}
