using CashFlow.Communication.Requests;
using CashFlow.Exception.Resource;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses;
public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidator()
    {
        RuleFor(e => e.Name).NotEmpty().WithMessage(ResourceExpense.NAME_EMPTY);
        RuleFor(e => e.Amount).GreaterThan(0).WithMessage(ResourceExpense.AMOUNT_GREATER_ZERO);
        RuleFor(e => e.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceExpense.EXPENSE_DATE_IN_FUTURE);
        RuleFor(e => e.PaymentType).IsInEnum().WithMessage(ResourceExpense.INVALID_PAYMENT_TYPE);
    }

}
