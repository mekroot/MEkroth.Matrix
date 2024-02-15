using MEkroth.Matrix.Api.StatusMatrices.Contracts;

namespace MEkroth.Matrix.Api.StatusMatrices.Mappers
{
    public static class GetStatusMatricesResponseMapper
    {
        public static StatusMatrixResponse MapToSingleRespone(this StatusMatrix statusMatrix)
        {
            var statusGrouped = statusMatrix.Statuses.GroupBy(s => s);
            var statusMatrixResponse = new StatusMatrixResponse()
            {
                Id = statusMatrix.Id.ToString(),
                Name = statusMatrix.Name,
            };

            foreach (var item in statusGrouped)
            {
                statusMatrixResponse.Statuses.Add(new StatusResponse(item.FirstOrDefault()) { Count = item.Count() });
            }

            return statusMatrixResponse;
        }

        public static StatusMatrixResponse[] MapToRespone(this List<StatusMatrix> statusMatrices)
        {
            var statusResponses = new List<StatusMatrixResponse>();

            foreach (var statusMatrix in statusMatrices)
            {
                statusResponses.Add(statusMatrix.MapToSingleRespone());
            }

            return statusResponses.ToArray();
        }
    }
}
