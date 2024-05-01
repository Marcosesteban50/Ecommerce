using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        

        [Required(ErrorMessage ="Ingrese nombre completo")]
        public string? NombreCompleto { get; set; }

        [EmailAddress(ErrorMessage ="Se requiere Correo")]

        public string? Correo { get; set; }

        [Required(ErrorMessage ="Ingrese contrasena")]

        //esto es para que se vea en codigo de puntitos la contrasena
        //[PasswordPropertyText]

        public string? Clave { get; set; }
        
        [Required(ErrorMessage ="Ingrese confirmar contrasena")]
        public string? ConfirmarClave { get; set; }
        

        public string? Rol { get; set; }

       

        

    }
}
