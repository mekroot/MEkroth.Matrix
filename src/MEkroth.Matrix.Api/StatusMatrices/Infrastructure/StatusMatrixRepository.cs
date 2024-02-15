using MEkroth.Matrix.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace MEkroth.Matrix.Api.StatusMatrices.Infrastructure
{
    public sealed class StatusMatrixRepository : IStatusMatrixRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public StatusMatrixRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteAsync(StatusMatrix removeMatrix, CancellationToken cancellationToken)
        {
            _dbContext.StatusMatrices.Entry(removeMatrix).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return await _dbContext.SaveChangesAsync(cancellationToken) == 1;
        }

        public async Task<List<StatusMatrix>> GetStatusMatricesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.StatusMatrices.ToListAsync(cancellationToken);
        }

        public async Task<StatusMatrix> GetStatusMatrixAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.StatusMatrices.AsNoTracking().FirstOrDefaultAsync(sm => sm.Id == id, cancellationToken);
        }

        public async Task<bool> SaveAsync(StatusMatrix statusMatrix, bool exists, CancellationToken cancellationToken)
        {
            if (exists)
            {
                _dbContext.StatusMatrices.Entry(statusMatrix).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                await _dbContext.StatusMatrices.AddAsync(statusMatrix);
            }

            return await _dbContext.SaveChangesAsync(cancellationToken) == 1;
        }
    }
}
