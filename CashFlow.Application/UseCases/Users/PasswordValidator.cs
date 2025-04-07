using CashFlow.Exception.Resource;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Expenses;
public partial class PasswordValidator<T> : IPropertyValidator<T, string>
{
    public string Name => "PasswordValidator";
    private static string OBJECT_KEY => "ErrorMessage";

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{OBJECT_KEY}}}";
    }

    public bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(OBJECT_KEY, ResourceUser.INVALID_PASSWORD);
            return false;
        }

        if (!UpperCaseLetter().IsMatch(password) ||
            !LowCaseLetter().IsMatch(password) ||
            !HasNumber().IsMatch(password) ||
            !HasSymbols().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(OBJECT_KEY, ResourceUser.INVALID_PASSWORD);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();
    
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowCaseLetter();
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex HasNumber();
    
    [GeneratedRegex(@"[!\@\#\$%\&\(\)\{\}\.\,]+")]
    private static partial Regex HasSymbols();
}
