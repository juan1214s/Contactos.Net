using System.ComponentModel.DataAnnotations;

namespace ContactosApi.Models.User
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no es válido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public required string Password { get; set; }
    }
}
