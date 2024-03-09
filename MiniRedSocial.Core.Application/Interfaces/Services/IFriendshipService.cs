using MiniRedSocial.Core.Application.ViewModels.Friendship;
using MiniRedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Interfaces.Services
{
    public interface IFriendshipService : IGenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>
    {
        Task<List<FriendshipViewModel>> GetFriendshipsByUserId(string userId);
        Task<bool> AreFriends(string usedId, string friendId);
    }
}
