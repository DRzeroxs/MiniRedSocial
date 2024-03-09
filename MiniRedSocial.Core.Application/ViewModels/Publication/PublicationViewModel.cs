using MiniRedSocial.Core.Application.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.Publication
{
    public class PublicationViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string? Url { get; set; }
        public int Tipe {  get; set; }
        public string? UserId { get; set; }
        public string? UserImg {  get; set; }
        public DateTime Date { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}
