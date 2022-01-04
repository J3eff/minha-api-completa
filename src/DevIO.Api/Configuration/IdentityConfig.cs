using DevIO.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DevIO.Api.Configuration
{
    public static class IdentityConfig 
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>() //Porque passar o IdentityRole -> Porque podemos costumizar minha pólitica de roles e passar uma classe manipulado por mim.
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); // Ele vai adicionar o recurso para poder gerar Tokens -> idenntificação por e-mail, reset de senha e etc...

            return services;
        }
    }
}
