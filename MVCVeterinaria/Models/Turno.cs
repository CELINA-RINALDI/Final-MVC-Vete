using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCVeterinaria.Models
{
    [Table("Turno")]
    public class Turno
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column(TypeName = "time")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);

        [Required(ErrorMessage = "Ingrese un nombre válido"), MaxLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre de su mascota:")]
        public string NombreMascota { get; set; }

        [Display(Name = "Tipo de mascota")]
        public TipoMascota TipoMascota { get; set; }

        [Required(ErrorMessage = "Ingrese una raza válida"), MaxLength(30)]
        [DataType(DataType.Text)]
        public string Raza { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Ingrese una edad válido")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre válido"), MaxLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre del dueño")]
        public string NombreDuenio { get; set; }

        [Required(ErrorMessage = "Ingrese un número de celular válido")]
        [DataType(DataType.PhoneNumber)]
        [Range(1100000000, 1199999999, ErrorMessage = "Ingrese un número de celular válido")]
        public string Celular { get; set; }

        public int MedicoId { get; set; }

        [ForeignKey("MedicoId")]
        public Medico Medico { get; set; }

    }
}
