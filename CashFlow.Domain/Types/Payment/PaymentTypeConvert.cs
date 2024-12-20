namespace CashFlow.Domain.Types.Payment;
public static class PaymentTypeConvert
{
    public static string Convert(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => PaymentTypeResource.CASH,
            PaymentType.CreditCard => PaymentTypeResource.CREDIT_CARD,
            PaymentType.DebitCard => PaymentTypeResource.DEBIT_CARD,
            PaymentType.EletronicTransfer => PaymentTypeResource.ELETRONIC_TRANSFER,
            _ => string.Empty,
        };
    }
}
