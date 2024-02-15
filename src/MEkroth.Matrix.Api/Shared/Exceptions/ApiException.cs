using System.Net;

namespace MEkroth.Matrix.Api.Shared.Exceptions
{
    public abstract class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
