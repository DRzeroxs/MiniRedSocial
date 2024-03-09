using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Domain.Entities;
using MiniRedSocial.infrastructure.Identity.Entities;
using MiniRedSocial.Infrastructure.Persistence.Contexts;

namespace MiniRedSocial.Infrastructure.Persistence.Repositories
{
    public class HiloRepository : GenericRepository<Hilo>, IHiloRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HiloRepository(UserManager<ApplicationUser> userManager, ApplicationContext context) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Hilo>> GetByMessageIdAsync(int messageId)
        {
            var hilo = await _context.Hilos
                   .Where(entiry => entiry.MessageId == messageId)
                   .ToListAsync();

            return hilo;

        }

        public async Task<string> GetUserImgUrlbyId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return "";
            }

            return user.ImageUrl;
        }
    }
}
