using FluentValidation;
using LanguageExt.Common;
using MediatR;
using MEkroth.Matrix.Api.Shared.Errors;
using MEkroth.Matrix.Api.Shared.Extensions;
using MEkroth.Matrix.Api.StatusMatrices.Contracts;
using MEkroth.Matrix.Api.StatusMatrices.Exceptions;
using MEkroth.Matrix.Api.StatusMatrices.Infrastructure;
using MEkroth.Matrix.Api.StatusMatrices.Mappers;
using System.Net;

namespace MEkroth.Matrix.Api.StatusMatrices
{
    public static class SaveStatusMatrix
    {
        /// <summary>
        /// Uses for register the SaveStatusMatrix endpoint
        /// </summary>
        /// <param name="app">Current webapplication instant to register the endpoint to</param>
        /// <returns>Web application with registered endpoint</returns>
        public static IApplicationBuilder UseSaveStatusMatrixEndpoint(this WebApplication app)
        {
            app.MapPost(StatusMatricesRoutes.Base, async (SaveStatusMatrixRequest payload, ISender sender) =>
            {
                if (Guid.TryParse(payload.Id, out Guid id))
                {
                    var statuses = new List<Status>();
                    foreach (var item in payload.Statuses)
                    {
                        statuses.Add(item.Cast());
                    }

                    var result = await sender.Send(new Command(id, payload.Name, statuses.ToArray()));

                    return result.Match(
                        (statusMatrixId) => Results.Ok(statusMatrixId),
                        (error) => ErrorResult.HandleResponse(error)
                    );
                }

                return Results.BadRequest("Invalid id type.");

            }).AddApiDocumentation<Guid>(
                tag: "StatusMatrices",
                summary: "Saves status matrix.",
                description: "Saves and update status matrix. Returns save id.",
                responses: new Dictionary<int, string>
                {
                    { (int)HttpStatusCode.NotFound, "Status matrix doesn't exists." },
                    { (int)HttpStatusCode.BadRequest, "Validation errors. Invalid Id. Missing name or missing Status" },
                });

            return app;
        }

        public sealed record Command(Guid Id, string Name, Status[] Statuses) : IRequest<Result<StatusMatrixResponse>>;

        /// <summary>
        /// Command validator created with help of FluentValidation.
        /// Validates if the Name i not null or empty.
        /// </summary>
        public sealed class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // Name should not be null or empty
                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("Please enter a name for the status matrix.");

                // Statuses can't be an empty array
                RuleFor(c => c.Statuses)
                    .NotEmpty()
                    .WithMessage("Please specify atleast 1 status.");
            }
        }

        internal sealed class CommandHandler : IRequestHandler<Command, Result<StatusMatrixResponse>>
        {
            private readonly IStatusMatrixRepository _statusMatrixRepository;
            private readonly IValidator<Command> _validator;

            public CommandHandler(IStatusMatrixRepository statusMatrixRepository, IValidator<Command> validator)
            {
                _statusMatrixRepository = statusMatrixRepository;
                _validator = validator;
            }

            public async Task<Result<StatusMatrixResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    // Creates a faulty response with the validation errors coming from validator.
                    return new Result<StatusMatrixResponse>(new ValidationException(validationResult.Errors));
                }

                StatusMatrix statusMatrix = await _statusMatrixRepository.GetStatusMatrixAsync(request.Id, cancellationToken) ?? new();
                bool exists = true;
                if (statusMatrix.Id == Guid.Empty)
                {
                    exists = false;
                    statusMatrix.Id = Guid.NewGuid();
                }

                statusMatrix.Statuses = request.Statuses;
                statusMatrix.Name = request.Name;

                try
                {
                    await _statusMatrixRepository.SaveAsync(statusMatrix, exists, cancellationToken);
                }
                catch (Exception e)
                {
                    return new Result<StatusMatrixResponse>(StatusMatrixErrors.CouldNotSave(e));
                }

                return statusMatrix.MapToSingleRespone();
            }
        }
    }
}
