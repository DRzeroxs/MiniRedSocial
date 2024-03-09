using MiniRedSocial.Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniRedSocial.Core.Domain.Entities
{
    public class Message : AuditableBaseEntity
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }

        //llave foranea
        public string? UserId { get; set; }

        [ForeignKey("Publication")]
        public int PublicationId { get; set; }

        //Conductor
        public Publication Publication { get; set; }

        [InverseProperty("Message")]
        public ICollection<Hilo> Hilos { get; set; }
    }
}
