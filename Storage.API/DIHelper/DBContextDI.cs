using Microsoft.EntityFrameworkCore;
using Storage.Data;

namespace Storage.API.DIHelper;

public static class DBContextDI
{
    public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(option =>
                               option.UseSqlServer(connection)
                           );
    }
}
