using DevIO.Api.Data;
using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders(); // Ele vai adicionar o recurso para poder gerar Tokens -> idenntificação por e-mail, reset de senha e etc...

            // JWT
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Toda vez que esta autenticando alguem, o padrão e gerar um token.
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //Valida o token.
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true; // Ele requer que a pessoa esta vindo, esta em HTTPS quando TRUE.
                x.SaveToken = true; // Ele guarda o token quando autenticado no HttpAuthenticationProperties feita com sucesso.
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Ele valida quem esta emitindo, tendo que ser o mesmo que apresentado no token
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Configuração da chave -> Ele transforma a chave para uma criptografada
                    ValidateIssuer = true,
                    ValidateAudience = true, // Onde o Token é valido 
                    ValidAudience = appSettings.ValidoEm, // Dizer qual é a Audiencia -> Se o token não tiver essa informação ele não esta valido
                    ValidIssuer = appSettings.Emissor // Dizer qual e o Issuer -> Se o token não tiver essa informação ele não esta valido
                };

            });

            return services;
        }
    }
}
