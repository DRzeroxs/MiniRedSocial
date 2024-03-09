using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Application.ViewModels.Message;
using MiniRedSocial.Core.Domain.Entities;
using MiniRedSocial.infrastructure.Identity.Entities;
using MiniRedSocial.Infrastructure.Persistence.Contexts;

namespace MiniRedSocial.Infrastructure.Persistence.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageRepository(UserManager<ApplicationUser> userManager, ApplicationContext context) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Message>> GetByPublicationIdAsync (int publicationId)
        {
            var messages = await _context.Messages
                   .Where(entiry => entiry.PublicationId == publicationId)
                   .ToListAsync();

            return messages;
            
        }

        public async Task<string> GetUserImgUrlbyId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return "";
            }

            return user.ImageUrl;
        }
    }
}
