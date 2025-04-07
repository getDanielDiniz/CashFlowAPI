using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Exception.Resource;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users;
public class UserValidator: AbstractValidator<RequestRegisterUser>
{
    public UserValidator()
    {
        RuleFor(e => e.Email).NotEmpty().WithMessage(ResourceUser.EMPTY_EMAIL).EmailAddress().WithMessage(ResourceUser.INVALID_EMAIL_ADDRESS);
        RuleFor(e => e.Name).NotEmpty().WithMessage(ResourceUser.EMPTY_NAME);
        RuleFor(e => e.Password).SetValidator(new PasswordValidator<RequestRegisterUser>());
        
    }
}
