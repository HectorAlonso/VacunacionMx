using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vacunacion.Models
{
    public class Registro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int FolioId { get; set; }

        [Display(Name = "Fecha de Vacunacion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime FechaVacunacion { get; set; } 

        [Display(Name = "Vacuna")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int VacunaId { get; set; }

        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int MunicipioId { get; set; }

        [Display(Name = "Dosis")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int DosisId { get; set; }

        public string Municipio { get; set; }
        public string Vacuna { get; set; }
        public string Dosis { get; set; }
    }
}