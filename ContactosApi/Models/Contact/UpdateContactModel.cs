using System.ComponentModel.DataAnnotations;

namespace ContactosApi.Models.Contact
{
    public class UpdateContactModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no es válido")]
        public string Email { get; set; }
    }
}
