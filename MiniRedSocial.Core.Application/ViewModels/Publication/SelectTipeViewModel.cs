using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.Publication
{
    public class SelectTipeViewModel
    {
        [Required]
        [Range(1, 3, ErrorMessage = "Seleccione un tipo de publicacion")]
        public int Tipe {  get; set; }
    }
}
