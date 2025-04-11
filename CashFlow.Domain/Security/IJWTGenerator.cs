using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Security;
public interface IJWTGenerator
{
    string GenerateJWTToken(User user);
}
