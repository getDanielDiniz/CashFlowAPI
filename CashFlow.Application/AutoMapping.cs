﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
        
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
    }
}