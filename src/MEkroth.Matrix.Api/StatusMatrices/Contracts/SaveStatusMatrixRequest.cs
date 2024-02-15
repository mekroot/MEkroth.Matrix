namespace MEkroth.Matrix.Api.StatusMatrices.Contracts
{
    public sealed record SaveStatusMatrixRequest(string Id, string Name, StatusRequest[] Statuses);
}
