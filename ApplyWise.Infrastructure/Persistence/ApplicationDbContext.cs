using ApplyWise.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplyWise.Infrastructure.Persistence;

public interface IEntidadeAuditavel
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}

public class ApplicationDbContext: DbContext
{
    public DbSet<JobApplication> Jobs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entradas = ChangeTracker.Entries<IEntidadeAuditavel>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entrada in entradas)
        {
            if (entrada.State == EntityState.Added)
            {
                entrada.Entity.CreatedAt = DateTime.UtcNow;
            }

            entrada.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}