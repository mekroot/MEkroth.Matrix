using MEkroth.Matrix.Api.Database.ValueConverters;
using MEkroth.Matrix.Api.StatusMatrices;
using Microsoft.EntityFrameworkCore;

namespace MEkroth.Matrix.Api.Database
{
    public class SqlLiteApplicationDbContext : DbContext, IApplicationDbContext
    {
        private const int DefaultMatrixRows = 5;
        private const int DefaultMatrixCols = 5;

        public SqlLiteApplicationDbContext(DbContextOptions<SqlLiteApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<StatusMatrix> StatusMatrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusMatrix>()
                .Property(sm => sm.Statuses)
                .HasConversion(new StatusArrayConverter());

            modelBuilder.Entity<StatusMatrix>()
                .HasData(Generate());
        }

        private StatusMatrix[] Generate()
        {
            var statusMatrices = new List<StatusMatrix>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                var matrixSize = DefaultMatrixRows * DefaultMatrixCols;
                var statuses = new List<Status>();
                for (int si = 0; si < matrixSize; si++)
                {
                    statuses.Add((Status)random.Next(0, 4));
                }

                statusMatrices.Add(new StatusMatrix { Id = Guid.NewGuid(), Name = $"Status Matrix {i + 1}", Statuses = statuses.ToArray() });
            }

            return statusMatrices.ToArray();
        }
    }
}
