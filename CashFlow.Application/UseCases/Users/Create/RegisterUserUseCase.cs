using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security;
using CashFlow.Exception.BaseExceptions;
using CashFlow.Exception.Resource;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Create;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IWriteOnlyUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordCriptography _passwordCriptography;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadOnlyUserRespository _readOnlyUserRespository;

    public RegisterUserUseCase(IWriteOnlyUserRepository repository,
        IMapper mapper, IPasswordCriptography passwordCriptography,
        IUnitOfWork unitOfWork, IReadOnlyUserRespository readOnlyUserRespository)
    {
        _mapper = mapper;
        _repository = repository;
        _passwordCriptography = passwordCriptography;
        _unitOfWork = unitOfWork;
        _readOnlyUserRespository = readOnlyUserRespository;
    }

    public async Task<ResponseUserRegistered> Execute(RequestRegisterUser newUser)
    {
        await Validate(newUser);

        User user = _mapper.Map<User>(newUser) ?? throw new ArgumentNullException(nameof(newUser));
        // Cryptography of user's password
        user.Password = _passwordCriptography.Encrypt(newUser.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _repository.RegisterUser(user);
        await _unitOfWork.Commit();

        return new ResponseUserRegistered(email: user.Email);

    }

    private async Task Validate(RequestRegisterUser user) {
        UserValidator validationRules = new();
        var result = validationRules.Validate(user);
        var isEmailUsed = await _readOnlyUserRespository.UsedEmail(user.Email);

        if (isEmailUsed) {
            result.Errors.Add(new ValidationFailure { ErrorMessage = ResourceUser.EMAIL_ALREADY_USED });
        }

        if (result.IsValid == false) {
            List<string> errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnUserValidation(errors);
        }
    }
}
