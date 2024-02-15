using FluentValidation;
using LanguageExt.Common;
using MediatR;
using MEkroth.Matrix.Api.Shared.Errors;
using MEkroth.Matrix.Api.Shared.Extensions;
using MEkroth.Matrix.Api.StatusMatrices.Exceptions;
using MEkroth.Matrix.Api.StatusMatrices.Infrastructure;
using System.Net;

namespace MEkroth.Matrix.Api.StatusMatrices
{
    public static class DeleteStatusMatrix
    {
        /// <summary>
        /// Uses for register the DeleteStatusMatrix endpoint
        /// </summary>
        /// <param name="app">Current webapplication instant to register the endpoint to</param>
        /// <returns>Web application with registered endpoint</returns>
        public static IApplicationBuilder UseDeleteStatusMatrixEndpoint(this WebApplication app)
        {
            app.MapDelete("api/status-matrices/{id:guid}",
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteStatusMatrix.Command(id));

                    return result.Match(
                        success => Results.Accepted(value: id),
                        error => ErrorResult.HandleResponse(error));
                }
            ).AddApiDocumentation<string>(
                tag: "StatusMatrices",
                summary: "Delete status matrix.",
                description: "Delete the specificed status matrix from database.",
                responses: new Dictionary<int, string>
                {
                    { (int)HttpStatusCode.Accepted, "Accepted"},
                    { (int)HttpStatusCode.BadRequest, "Status matrix doesn't exists." },
                    { (int)HttpStatusCode.InternalServerError, "Internal server error" },
                });

            return app;
        }

        public record Command(Guid Id) : IRequest<Result<Guid>>;

        /// <summary>
        /// Command validator created with help of FluentValidation.
        /// Validates if the Id i not null or empty.
        /// </summary>
        public sealed class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty).WithMessage("Has not a valid id.");
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Command, Result<Guid>>
        {
            private readonly IStatusMatrixRepository _statusMatrixRepository;
            private readonly IValidator<Command> _validator;

            public CommandHandler(IStatusMatrixRepository statusMatrixRepository, IValidator<Command> validator)
            {
                _statusMatrixRepository = statusMatrixRepository;
                _validator = validator;
            }

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    // Creates a faulty response with the validation errors coming from validator.
                    return new Result<Guid>(new ValidationException(validationResult.Errors));
                }

                StatusMatrix exists = await _statusMatrixRepository.GetStatusMatrixAsync(request.Id, cancellationToken);

                if (exists == null)
                {
                    return new Result<Guid>(StatusMatrixErrors.NotFound);
                }

                try
                {
                    var success = await _statusMatrixRepository.DeleteAsync(exists, cancellationToken);
                    if (success)
                    {
                        return exists.Id;
                    }

                    return new Result<Guid>(StatusMatrixErrors.CouldNotDelete(null));
                }
                catch (Exception ex)
                {
                    return new Result<Guid>(StatusMatrixErrors.CouldNotDelete(ex));
                }
            }
        }
    }
}
