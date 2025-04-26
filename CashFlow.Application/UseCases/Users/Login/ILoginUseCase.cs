using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.Users.Login;
public interface ILoginUseCase
{
    public Task<ResponseUserRegistered> Execute(string email, string password);
}
