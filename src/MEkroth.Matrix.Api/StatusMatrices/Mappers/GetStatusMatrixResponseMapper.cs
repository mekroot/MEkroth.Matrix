using MEkroth.Matrix.Api.StatusMatrices.Contracts;

namespace MEkroth.Matrix.Api.StatusMatrices.Mappers
{
    public static class GetStatusMatrixResponseMapper
    {
        public static GetStatusMatrixResponse MapToRespone(this StatusMatrix statusMatrix)
        {
            return new GetStatusMatrixResponse
            {
                Id = statusMatrix.Id.ToString(),
                Name = statusMatrix.Name,
                Statuses = statusMatrix.Statuses.Select(status => new StatusResponse(status)).ToArray(),
            };
        }
    }
}
