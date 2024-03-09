using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniRedSocial.Core.Application.Interfaces.Services;
using MiniRedSocial.Core.Domain.Settings;
using MiniRedSocial.Infrastructure.Shared.Services;

namespace MiniRedSocial.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
