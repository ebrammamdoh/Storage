using Infrastructure.Data.Storage;
using Microsoft.EntityFrameworkCore;
using Storage.Data.Entities;
using Storage.Data.Entities.Lookups;

namespace Storage.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    #region Lookups
    public virtual DbSet<StorageProviderType> StorageProviderTypes { get; set; }
    #endregion

    public virtual DbSet<FileMetaData> FileMetaData { get; set; }


    public override int SaveChanges()
    {
        var entries = ChangeTracker
        .Entries()
        .Where(e => (e.Entity is BaseEntity<int> || e.Entity is BaseEntity<ulong> || e.Entity is BaseEntity<long> || e.Entity is BaseEntity<Guid> || e.Entity is BaseEntity<string>) && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Modified:
                    ((BaseEntity<int>)entityEntry.Entity).UpdatedAt = DateTime.Now;
                    break;
                case EntityState.Added:
                    ((BaseEntity<int>)entityEntry.Entity).CreatedAt = DateTime.Now;
                    break;
                default:
                    break;
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
       .Entries()
       .Where(e => (e.Entity is BaseEntity<int> || e.Entity is BaseEntity<ulong> || e.Entity is BaseEntity<long> || e.Entity is BaseEntity<Guid> || e.Entity is BaseEntity<string>) && (
               e.State == EntityState.Added
               || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Modified:
                    if (entityEntry.Entity is BaseEntity<int>)
                        ((BaseEntity<int>)entityEntry.Entity).UpdatedAt = DateTime.Now;
                    else if (entityEntry.Entity is BaseEntity<Guid>)
                        ((BaseEntity<Guid>)entityEntry.Entity).UpdatedAt = DateTime.Now;
                    break;
                case EntityState.Added:
                    if (entityEntry.Entity is BaseEntity<int>)
                        ((BaseEntity<int>)entityEntry.Entity).CreatedAt = DateTime.Now;
                    else if (entityEntry.Entity is BaseEntity<Guid>)
                        ((BaseEntity<Guid>)entityEntry.Entity).CreatedAt = DateTime.Now;
                    break;
                default:
                    break;
            }
        }

        return await base.SaveChangesAsync();
    }
}
