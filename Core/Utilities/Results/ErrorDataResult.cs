using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ErrorDataResult(T data) : base(data, false)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ErrorDataResult(string message) : base(default, false, message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ErrorDataResult() : base(default, false)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}