using Microsoft.EntityFrameworkCore;
using MiniRedSocial.Core.Application.Interfaces.Repositories;
using MiniRedSocial.Core.Domain.Entities;
using MiniRedSocial.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Infrastructure.Persistence.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        private readonly ApplicationContext _context;

        public FriendshipRepository (ApplicationContext context) : base (context)
        {
            _context = context;
        }

        public async Task<Friendship> GetFriendshipByUserIds(string userId, string friendId)
        {
            return await _context.Friendships.FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));
        }
    }
}
