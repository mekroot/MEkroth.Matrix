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
    public static class GetStatusMatrix
    {
        /// <summary>
        /// Uses for register the GetStatusMatrix endpoint
        /// </summary>
        /// <param name="app">Current webapplication instant to register the endpoint to</param>
        /// <returns>Web application with registered endpoint</returns>
        public static IApplicationBuilder UseGetStatusMatrixEndpoint(this WebApplication app)
        {
            app.MapGet(StatusMatricesRoutes.Base + "/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetStatusMatrix.Query(id));

                return result.Match(
                    (statusMatrix) => Results.Ok(statusMatrix.MapToRespone()),
                    (error) => ErrorResult.HandleResponse(error)
                );
            }).AddApiDocumentation<GetStatusMatrixResponse>(
                tag: "StatusMatrices",
                summary: "Returns Status Matrix by id",
                description: "Return an empty objekt if the id is type of a Empty Guid. Otherwise it",
                responses: new Dictionary<int, string>
                {
                    { (int)HttpStatusCode.NotFound, "Status matrix doesn't exists." },
                });

            return app;
        }

        public record Query(Guid Id) : IRequest<Result<StatusMatrix>>;

        internal sealed class QueryHandler : IRequestHandler<Query, Result<StatusMatrix>>
        {
            private readonly IStatusMatrixRepository _statusMatrixRepository;

            public QueryHandler(IStatusMatrixRepository statusMatrixRepository)
            {
                _statusMatrixRepository = statusMatrixRepository;
            }

            public async Task<Result<StatusMatrix>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.Id == Guid.Empty)
                {
                    return new StatusMatrix();
                }

                var result = await _statusMatrixRepository.GetStatusMatrixAsync(request.Id, cancellationToken);

                if (result == null)
                {
                    return new Result<StatusMatrix>(StatusMatrixErrors.NotFound);
                }

                return result;
            }
        }
    }
}
