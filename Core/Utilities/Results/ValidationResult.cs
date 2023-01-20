using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Results
{
    public class ValidationResult : Result
    {
        public ValidationResult(string message) : base(false, message)
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }

        public ValidationResult() : base(false)
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }
    }
}