using ContactsApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public IActionResult BadRequest(ModelStateDictionary modelState)
        {
            var errors = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList());

            return new ObjectResult(new ErrorResponse("400", errors))
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
