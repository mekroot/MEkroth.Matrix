namespace MEkroth.Matrix.Api.StatusMatrices.Infrastructure
{
    public interface IStatusMatrixRepository
    {
        Task<StatusMatrix> GetStatusMatrixAsync(Guid id, CancellationToken cancellationToken);
        Task<List<StatusMatrix>> GetStatusMatricesAsync(CancellationToken cancellationToken);
        Task<bool> SaveAsync(StatusMatrix statusMatrix, bool exists, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(StatusMatrix statusMatrix, CancellationToken cancellationToken);
    }
}
