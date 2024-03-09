using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.Friendship
{
    public class SaveFriendshipViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set;}
    }
}
