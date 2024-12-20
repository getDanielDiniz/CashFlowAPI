using Bogus;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Types.PaymentType;

namespace CommonTests.Utilities;

public class RequestExpenseJsonBuilder
{

    public static RequestExpenseJson Build()
    {
        Faker faker = new();
        PaymentType paymentType = new();

        return new RequestExpenseJson()
        {
            Amount = faker.Random.Decimal(min:1, max:1000),
            Date = faker.Date.Past(),
            Description = faker.Lorem.Sentence(),
            Name = faker.Commerce.ProductName(),
            PaymentType = faker.PickRandom<PaymentType>()
        };
    }
}
