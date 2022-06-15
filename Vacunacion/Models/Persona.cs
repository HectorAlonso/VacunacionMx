using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacunacion.Models
{
    public class Persona
    {
        public int Folio { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(18,100)]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:13, MinimumLength =13,ErrorMessage ="Debe de ingresar los 13 Caracteres del RFC.")]
        public string RFC { get; set; }
    }
}