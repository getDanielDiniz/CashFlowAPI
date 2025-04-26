using CashFlow.Application.UseCases.Users.Login;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(statusCode:StatusCodes.Status200OK, type:typeof(RequestRegisterUser))]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, type: typeof(ResponseErrorJson))]
    public async Task<IActionResult> Login([FromBody]RequestAuthenticationJson auth, [FromServices] ILoginUseCase useCase)
    {
        var token = await useCase.Execute(auth.Email, auth.Password);

        return Ok(token);
    }
}
