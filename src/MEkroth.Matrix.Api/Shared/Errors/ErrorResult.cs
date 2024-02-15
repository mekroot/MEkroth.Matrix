using MEkroth.Matrix.Api.Shared.Exceptions;

namespace MEkroth.Matrix.Api.Shared.Errors
{
    public static class ErrorResult
    {
        public static IResult HandleResponse(Exception error)
        {
            if (error is FluentValidation.ValidationException validationExceptin)
            {
                var validationErrors = new Dictionary<string, string[]>();

                foreach (var validationError in validationExceptin.Errors)
                {
                    validationErrors.Add(validationError.PropertyName, new[] { validationError.ErrorMessage });
                }

                return Results.ValidationProblem(validationErrors);
            }

            if (error is ApiException apiException)
            {
                switch (apiException.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return Results.NotFound(new Error { StatusCode = (int)apiException.StatusCode, Detail = apiException.Message });
                    case System.Net.HttpStatusCode.BadRequest:
                        return Results.BadRequest(new Error { StatusCode = (int)apiException.StatusCode, Detail = apiException.Message });
                    default:
                        return Results.Problem(statusCode: (int)apiException.StatusCode, detail: apiException.Message);
                }
            }

            return Results.Problem(statusCode: 500, detail: "An internal server has occurred");
        }

    }
}
