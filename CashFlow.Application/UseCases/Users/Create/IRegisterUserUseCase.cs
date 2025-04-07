using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.Users.Create;
public interface IRegisterUserUseCase
{
    Task<ResponseUserRegistered> Execute(RequestRegisterUser newUser);
}
