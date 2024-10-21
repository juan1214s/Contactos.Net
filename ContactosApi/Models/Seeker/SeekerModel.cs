using System.ComponentModel.DataAnnotations;

namespace ContactosApi.Models.Seeker
{
    public class SeekerModel
    {
        [Required(ErrorMessage = "El ID del usuario al que se relaciona el contacto es requerido")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Name { get; set; }
    }
}
