using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.infrastructure.Identity.Entities;
using MiniRedSocial.infrastructure.Identity.Services;
using MiniRedSocial.Infrastructure.Persistence.Contexts;

namespace MiniRedSocial.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
            });

            services.AddAuthentication();
            #endregion

            #region Service
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
