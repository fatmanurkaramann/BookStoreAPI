using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore
{
    public static class Configuration
    {
        public static IConfiguration configuration;

        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {

                string tokenIssuer = configuration["Token:Issuer"];
                string tokenAudience = configuration["Token:Audience"];
                string tokenSecurityKey = configuration["Token:SecurityKey"];

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenIssuer,
                    ValidAudience = tokenAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

    }
}
