using MiniRedSocial.Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiniRedSocial.Core.Domain.Entities
{
    public class Friendship : AuditableBaseEntity
    {
        public string? UserId { get; set; }

        public string? FriendId { get; set; }

    }
}
