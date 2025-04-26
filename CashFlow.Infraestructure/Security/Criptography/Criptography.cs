using CashFlow.Domain.Security;
using BC = BCrypt.Net.BCrypt;
namespace CashFlow.Infraestructure.Security.Criptography;
internal class Criptography : IPasswordCriptography
{
    public string Encrypt(string password)
    {
        return BC.HashPassword(password);
    }

    public bool IsPasswordMatch(string password, string hash)
    {
        return BC.Verify(password,hash);
    }
}
