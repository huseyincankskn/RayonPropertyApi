using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Results
{
    public class ValidationDataResult<T> : DataResult<T>
    {
        public ValidationDataResult(T data, string message) : base(data, false, message)
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }

        public ValidationDataResult(T data) : base(data, false)
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }

        public ValidationDataResult(string message) : base(default, false, message)
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }

        public ValidationDataResult() : base(default, false)
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }
    }
}