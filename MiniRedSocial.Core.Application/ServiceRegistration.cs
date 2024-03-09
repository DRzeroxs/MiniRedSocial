using Microsoft.Extensions.DependencyInjection;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Application.Services;
using System.Reflection;

namespace MiniRedSocial.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IHiloService, HiloService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            services.AddTransient<IMessageService, MessageService>();
            #endregion
        }
    }
}
