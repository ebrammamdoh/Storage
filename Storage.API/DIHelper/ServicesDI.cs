using Storage.Data;
using Storage.Services.FactoryHelper;
using Storage.Services.Repositories;
using Storage.Services.StorageHelper;

namespace Storage.API.DIHelper;

public static class ServicesDI
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(ApplicationDbContext));
        services.AddScoped<IStorageFactoryHelper, StorageFactoryHelper>();
        services.AddScoped<IFileStorageHelper, FileStorageHelper>();
        
    }
}
