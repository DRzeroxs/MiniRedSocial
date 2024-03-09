using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un Telefono")]
        [DataType(DataType.Text)]
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
