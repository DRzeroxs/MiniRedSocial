using MiniRedSocial.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Domain.Entities
{
    public class Publication : AuditableBaseEntity
    {
        public string Content { get; set; }
        public string? Url { get; set; }
        public int Tipe { get; set; }
        public DateTime Date { get; set; }

        //llave foranea
        public string? UserId { get; set; }

        //Conductor
        [InverseProperty("Publication")]
        public ICollection<Message> Messages { get; set; }
    }
}
