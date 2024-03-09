using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.Hilo
{
    public class HiloViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int MessageId { get; set; }
        public string? UserId { get; set; }

        public string? UserImg { get; set; }
    }
}
