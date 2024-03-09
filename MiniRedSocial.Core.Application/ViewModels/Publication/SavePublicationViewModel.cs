using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MiniRedSocial.Core.Application.ViewModels.Publication
{
    public class SavePublicationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar algun texto")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public string? Url { get; set; }

        [Required(ErrorMessage = "Debe colocar algun tipo")]
        public int Tipe { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public string? UserId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
    }
}
