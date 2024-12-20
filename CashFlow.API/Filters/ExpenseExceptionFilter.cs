using CashFlow.Communication.Responses;
using CashFlow.Exception.BaseExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExpenseExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowBaseException cashFlowException)
        {
            HandleException(context, cashFlowException);
        }
        else { 
            ThrowUnknowError(context);
        }

    }

    private static void HandleException(ExceptionContext context, CashFlowBaseException cashFlowException)
    {
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;

        ResponseErrorJson response = new(cashFlowException.getErrors());

        context.Result = new ObjectResult(response);
    }

    private static void ThrowUnknowError(ExceptionContext context) {

        ResponseErrorJson response = new("Unknown Error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(response);
    }
}
