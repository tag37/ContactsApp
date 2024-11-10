using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Infrastructure
{
    public class ErrorResult : ObjectResult
    {
        public ErrorResult(string detail, int statusCode) : base(new ErrorResponse(detail, statusCode.ToString())
        )
        {
            this.StatusCode = statusCode;
        }
    }
}
