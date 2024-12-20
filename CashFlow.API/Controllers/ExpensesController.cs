using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.Read.ReadAll;
using CashFlow.Application.UseCases.Expenses.Read.ReadById;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(statusCode:StatusCodes.Status201Created, type: typeof(ResponseExpenseJson))]
    [ProducesResponseType(statusCode:StatusCodes.Status400BadRequest, type: typeof(ResponseErrorJson))]
    public async Task<IActionResult> Create([FromBody] RequestExpenseJson request,
        [FromServices] ICreateExpense create)
    {
        ResponseExpenseJson response =  await create.Execute(request);
        return Created(string.Empty,response);
    }

    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<ResponseShortExpenseJson>))]
    [ProducesResponseType(statusCode:StatusCodes.Status204NoContent, type:typeof(ResponseErrorJson))]
    public async Task<IActionResult> GetAll([FromServices] IReadAllExpenses readAll)
    {
        ResponseAllExpensesJson response = await readAll.Execute();

        if (response.Expenses.Count == 0)
        {
            return NoContent();
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<ResponseShortExpenseJson>))]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseErrorJson))]
    public async Task<IActionResult> GetByID([FromRoute]int id,
        [FromServices] IReadById readOne)
    {
        ResponseExpenseJson response = await readOne.Execute(id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseErrorJson))]
    public async Task<IActionResult> DeleteExpense([FromRoute] long id,
        [FromServices] IDeleteExpense delete)
    {
        await delete.Execute(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseErrorJson))]
    public async Task<IActionResult> Update([FromRoute] long id,
        [FromBody] RequestExpenseJson request,
        [FromServices] IUpdateExpense update)
    {
        await update.Execute(id,request);
        return NoContent();
    }
}
