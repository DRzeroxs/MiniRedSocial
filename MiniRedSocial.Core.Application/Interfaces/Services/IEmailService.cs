using MiniRedSocial.Core.Application.Dtos.Email;
using MiniRedSocial.Core.Domain.Settings;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
