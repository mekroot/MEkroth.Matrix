namespace MEkroth.Matrix.Api.StatusMatrices.Contracts
{
    public sealed class StatusMatrixResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<StatusResponse> Statuses { get; set; } = new();
    }
}
