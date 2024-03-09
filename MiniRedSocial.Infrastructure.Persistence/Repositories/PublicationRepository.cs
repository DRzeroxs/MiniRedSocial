using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Domain.Entities;
using MiniRedSocial.infrastructure.Identity.Entities;
using MiniRedSocial.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Infrastructure.Persistence.Repositories
{
    public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PublicationRepository(UserManager<ApplicationUser> userManager, ApplicationContext context) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Publication>> GetByUserIdAsync(string userId)
        {
            var publicaciones = await _context.Publications
                   .Where(entiry => entiry.UserId == userId)
                   .ToListAsync();

            return publicaciones;

        }

        public async Task<Publication> GetViewModelByIdAsync(int id)
        {
            var publication = await _context.Publications.Where(entity => entity.Id == id).FirstOrDefaultAsync();

            return publication;
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
