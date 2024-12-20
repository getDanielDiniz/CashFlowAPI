using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Types.PaymentType;
using CashFlow.Domain.Types.Payment;
using CashFlow.Exception.Resource;
using CommonTests.Utilities;
using FluentAssertions;
using FluentValidation.Results;

namespace ExpensesValidator.Test.Create;
public class CreateExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        RequestExpenseJson request = RequestExpenseJsonBuilder.Build();

        ExpenseValidator validationRules = new();

        ValidationResult result = validationRules.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("      ")]
    public void Error_Title_Empty(string? title)
    {
        RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
        request.Name = title;

        ExpenseValidator validationRules = new();

        ValidationResult result = validationRules.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceExpense.NAME_EMPTY);
    }

    [Fact]
    public void Error_Date_Future()
    {
        RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(2);

        ExpenseValidator validationRules = new();

        ValidationResult result = validationRules.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceExpense.EXPENSE_DATE_IN_FUTURE);
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)100;

        ExpenseValidator validationRules = new();

        ValidationResult result = validationRules.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceExpense.INVALID_PAYMENT_TYPE);
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(0)]
    public void Error_Amout_Invalid(int amount)
    {
        RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
        request.Amount = amount;

        ExpenseValidator validationRules = new();

        ValidationResult result = validationRules.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e=> e.ErrorMessage == ResourceExpense.AMOUNT_GREATER_ZERO);
    }
}
