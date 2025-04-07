namespace CashFlow.Domain.Repositories.Users;
public interface IReadOnlyUserRespository
{
    Task<bool> UsedEmail(string email);
}
