using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.Friendship
{
    public class FriendViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
