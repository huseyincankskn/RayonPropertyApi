using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ErrorResult() : base(false)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}