using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Storage.API.DIHelper;

public static class IdentityDI
{
    public static void RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "identity",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")))
                };
            });

        services.AddAuthorization();
    }
}
