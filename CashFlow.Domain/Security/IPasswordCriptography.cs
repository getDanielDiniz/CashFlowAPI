namespace CashFlow.Domain.Security;
public interface IPasswordCriptography
{
    string Encrypt(string password);
}
