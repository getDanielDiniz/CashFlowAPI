﻿namespace CashFlow.Communication.Responses.Expense;
public class ResponseShortExpenseJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
