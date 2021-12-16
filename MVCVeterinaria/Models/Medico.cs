using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCVeterinaria.Models
{
    public class Medico
    {
        [Key]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "El nombre del médico es obligatorio"), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del médico es obligatorio"), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La matricula es obligatoria")]
        [Range(0, 999, ErrorMessage = "Ingrese un número matricula válido")]
        [Display(Name = "Número de matricula")]
        public int NroMatricula { get; set; }
    }
}
