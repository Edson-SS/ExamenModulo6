using System.ComponentModel.DataAnnotations;

namespace MVCCapas2025.Models
{
    public class ProfesorModel
    {
        public int ProfesorID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        [EmailAddress]
        public string CorreoElectronico { get; set; }
    }
}
