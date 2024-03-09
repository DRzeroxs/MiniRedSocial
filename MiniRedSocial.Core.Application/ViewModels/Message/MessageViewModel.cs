using MiniRedSocial.Core.Application.ViewModels.Hilo;
using System.ComponentModel.DataAnnotations;

namespace MiniRedSocial.Core.Application.ViewModels.Message
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un mensaje")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int PublicationId { get; set; }
        public string? UserId { get; set; }

        public string? UserImg {  get; set; }

        public List<HiloViewModel> Respuestas { get; set; }
    }
}
