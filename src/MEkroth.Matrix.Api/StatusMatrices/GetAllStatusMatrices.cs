using LanguageExt.Common;
using MediatR;
using MEkroth.Matrix.Api.Shared.Extensions;
using MEkroth.Matrix.Api.StatusMatrices.Contracts;
using MEkroth.Matrix.Api.StatusMatrices.Infrastructure;
using MEkroth.Matrix.Api.StatusMatrices.Mappers;

namespace MEkroth.Matrix.Api.StatusMatrices
{
    public static class GetAllStatusMatrices
    {
        /// <summary>
        /// Uses for register the GetAllStatusMatrices endpoint
        /// </summary>
        /// <param name="app">Current webapplication instant to register the endpoint to</param>
        /// <returns>Web application with registered endpoint</returns>
        public static WebApplication UseGetAllStatusMatricesEndpoint(this WebApplication app)
        {
            app.MapGet(StatusMatricesRoutes.Base, async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllStatusMatrices.Query());

                return result.Match(
                    (response) => Results.Ok(response),
                    (error) => Results.BadRequest(error)
                );
            }).AddApiDocumentation<GetStatusMatricesResponse>(
                tag: "StatusMatrices",
                summary: "Returns all status matrices.",
                description: "Returns all current availble status matrices in the databas");

            return app;
        }

        public record Query() : IRequest<Result<StatusMatrixResponse[]>>;

        internal sealed class QueryHandler : IRequestHandler<Query, Result<StatusMatrixResponse[]>>
        {
            private readonly IStatusMatrixRepository _statusMatrixRepository;

            public QueryHandler(IStatusMatrixRepository statusMatrixRepository)
            {
                _statusMatrixRepository = statusMatrixRepository;
            }

            public async Task<Result<StatusMatrixResponse[]>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _statusMatrixRepository.GetStatusMatricesAsync(cancellationToken);

                return result.MapToRespone();
            }
        }
    }
}
