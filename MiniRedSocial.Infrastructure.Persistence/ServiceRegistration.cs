using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Infrastructure.Persistence.Contexts;
using MiniRedSocial.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration) {

            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("Default"),
                            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                    )
                );
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IHiloRepository, HiloRepository>();
            services.AddTransient<IPublicationRepository, PublicationRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IFriendshipRepository, FriendshipRepository>();
            #endregion
        }
    }
}
