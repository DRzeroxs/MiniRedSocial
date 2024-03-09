using MiniRedSocial.Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniRedSocial.Core.Domain.Entities
{
    public class Hilo : AuditableBaseEntity
    {
        public string Content { get; set; }
        public DateTime Date {  get; set; }

        public string? UserId {  get; set; }

        //Llave foranea
        [ForeignKey("Message")]
        public int MessageId { get; set; }

        //Conductor
        public Message Message { get; set; }
    }
}
