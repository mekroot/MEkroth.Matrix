using MEkroth.Matrix.Api.StatusMatrices;
using Microsoft.EntityFrameworkCore;

namespace MEkroth.Matrix.Api.Database
{
    public interface IApplicationDbContext
    {
        DbSet<StatusMatrix> StatusMatrices { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
