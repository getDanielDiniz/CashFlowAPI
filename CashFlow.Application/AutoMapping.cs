using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Communication.Responses.User;
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
        //Expense Mapping
        CreateMap<RequestExpenseJson, Expense>();

        //User Mapping
        CreateMap<RequestRegisterUser, User>();
    }

    private void EntityToResponse()
    {
        //User Mapping
        CreateMap<User, ResponseUserRegistered>();

        //Expense Mapping
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
    }
}
