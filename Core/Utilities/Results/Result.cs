using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
            StatusCode = StatusCodes.Status200OK;
            if (string.IsNullOrEmpty(Message))
            {
                Message = Success ? "The operation was successful" : "An error occurred during the operation.";
            }
        }

        public bool Success { get; }
        public string Message { get; }

        public int StatusCode { get; set; }
    }
}