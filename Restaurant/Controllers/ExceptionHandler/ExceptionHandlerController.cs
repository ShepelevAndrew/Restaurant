using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers.ExceptionHandler;

public class ExceptionHandlerController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: exception?.Message);
    }
}