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

        [Required(ErrorMessage = "Ingrese un nombre válido"), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un apellido válido"), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required]
        [Range(0, 999, ErrorMessage = "Ingrese un número matricula válido")]
        [Display(Name = "Número de matricula")]
        public int NroMatricula { get; set; }
    }
}
