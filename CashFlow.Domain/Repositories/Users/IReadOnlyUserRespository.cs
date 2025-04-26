using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users;
public interface IReadOnlyUserRespository
{
    Task<bool> UsedEmail(string email);

    Task<User?> GetUserByEmail(string email);
}
