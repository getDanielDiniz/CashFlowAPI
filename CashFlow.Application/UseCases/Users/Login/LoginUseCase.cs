using AutoMapper;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Exception.BaseExceptions;
using CashFlow.Domain.Security;
using CashFlow.Exception.Resource;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Users.Login;
public class LoginUseCase : ILoginUseCase
{
    private readonly IReadOnlyUserRespository _repository;
    private readonly IPasswordCriptography _passwordCriptography;
    private readonly IJWTGenerator _jwtGenerator;
    public LoginUseCase(IJWTGenerator generator,IReadOnlyUserRespository repository, IPasswordCriptography passwordCriptography)
    {
        _repository = repository;
        _jwtGenerator = generator;
        _passwordCriptography = passwordCriptography;
    }

    public async Task<ResponseUserRegistered> Execute(string email, string password)
    {
        User? user = await _repository.GetUserByEmail(email) ?? throw new InvalidCredentialsException(ResourceUser.UNAUTHORIZED);

        bool match = _passwordCriptography.IsPasswordMatch(password, user.Password);

        if (match == false)
        {
            throw new InvalidCredentialsException(ResourceUser.UNAUTHORIZED);
        }

        string Token = _jwtGenerator.GenerateJWTToken(user);

        return new ResponseUserRegistered(email, Token);
    }
}
