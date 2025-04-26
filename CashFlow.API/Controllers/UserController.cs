using CashFlow.Application.UseCases.Users.Create;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(ResponseUserRegistered))]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseErrorJson))]
    public async Task<IActionResult> RegisterUser([FromServices] IRegisterUserUseCase RegisterUser,
        [FromBody] RequestRegisterUser newUser)
    {
        ResponseUserRegistered RegisteredUser = await RegisterUser.Execute(newUser);
        return Created(string.Empty, RegisteredUser);
    }

}
