using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.Message
{
    public class SaveMessageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un mensaje")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int PublicationId { get; set; }
        public string? UserId { get; set; }
    }
}
