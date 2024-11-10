using ContactsApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers
{
    public class ApiBaseController : ControllerBase
    {

        [NonAction]
        public IActionResult BadRequest(string message)
        {
            return new ObjectResult(new ErrorResponse("400", message))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        [NonAction]
        public IActionResult NotFound(string message)
        {
            return new ObjectResult(new ErrorResponse("404", message))
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }

    }
}
