using Storage.Data;

namespace Storage.Services.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        ApplicationDbContext GetContext();
    }
}
