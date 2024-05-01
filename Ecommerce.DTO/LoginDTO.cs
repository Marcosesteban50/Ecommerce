using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class LoginDTO
    {

        [EmailAddress(ErrorMessage = "Se requiere Correo")]

        public string? Correo { get; set; }



        [Required(ErrorMessage = "Ingrese contrasena")]

        //esto es para que se vea en codigo de puntitos la contrasena
        //[PasswordPropertyText]

        public string? Clave { get; set; }
    }
}
