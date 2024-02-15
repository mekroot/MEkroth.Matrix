namespace MEkroth.Matrix.Api.StatusMatrices.Contracts
{
    public sealed class GetStatusMatrixResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public StatusResponse[] Statuses { get; set; }
    }
}
