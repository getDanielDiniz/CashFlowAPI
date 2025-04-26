using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories;
internal class UserRepository : IWriteOnlyUserRepository, IReadOnlyUserRespository
{
    internal readonly CashFlowDbContext _dbContext;

    public UserRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(us => us.Email == email);
    }

    public async Task RegisterUser(User user)
    {
        await _dbContext.Users.AddAsync(user);   
    }

    public async Task<bool> UsedEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(e => e.Email.Equals(email));
    }
}
