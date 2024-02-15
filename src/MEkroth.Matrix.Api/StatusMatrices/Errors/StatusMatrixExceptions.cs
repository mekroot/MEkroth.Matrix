using MEkroth.Matrix.Api.Shared.Exceptions;
using System.Net;

namespace MEkroth.Matrix.Api.StatusMatrices.Exceptions
{
    public static class StatusMatrixExceptions
    {
        public sealed class StatusMatrixNotFoundException : ApiException
        {
            /// <summary>
            /// Creates an not found request error when not found any system matrix.
            /// </summary>
            /// <param name="message">Error message to show user.</param>
            public StatusMatrixNotFoundException(string message) : base(HttpStatusCode.NotFound, message)
            {
            }

            /// <summary>
            /// Creates a internal server error if the request to get status matrix by an inner expeption.
            /// </summary>
            /// <param name="message">Error message to show user.</param>
            /// <param name="innerException">Inner exception catched when action.</param>
            public StatusMatrixNotFoundException(string message, Exception innerException) : base(HttpStatusCode.InternalServerError, message, innerException)
            {
            }
        }

        public sealed class StatusMatrixCouldNotDeleteException : ApiException
        {
            /// <summary>
            /// Creates a own instance of this without internal exceptions it will set status code to be BadRequest.
            /// </summary>
            /// <param name="message">Error message to show user.</param>
            /// <param name="innerException">Inner exception catched when action.</param>
            public StatusMatrixCouldNotDeleteException(string message) : base(HttpStatusCode.BadRequest, message)
            {
            }

            /// <summary>
            /// Create a internal server error when action to delete throws an exception
            /// </summary>
            /// <param name="message">Error message to show user.</param>
            /// <param name="innerException">Inner exception catched when action.</param>
            public StatusMatrixCouldNotDeleteException(string message, Exception innerException) : base(HttpStatusCode.InternalServerError, message, innerException)
            {
            }
        }

        public sealed class StatusMatrixCouldNotSaveException : ApiException
        {
            /// <summary>
            /// Creates a bad request if only a message error given.
            /// </summary>
            /// <param name="message">Error message to show user.</param>
            /// <param name="innerException">Inner exception catched when action.</param>
            public StatusMatrixCouldNotSaveException(string message) : base(HttpStatusCode.BadRequest, message)
            {
            }

            /// <summary>
            /// Create a internal server error when action to save throws an exception
            /// </summary>
            /// <param name="message">Error message to show user.</param>
            /// <param name="innerException">Inner exception catched when action.</param>
            public StatusMatrixCouldNotSaveException(string message, Exception innerException) : base(HttpStatusCode.InternalServerError, message, innerException)
            {
            }
        }
    }
}
