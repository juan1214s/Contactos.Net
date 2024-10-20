using ContactosApi.Models.Contact;
using ContactosApi.Services.contact;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class DeleteController : ControllerBase
    {
        private readonly DeleteContactService _deleteContact;
        private readonly ILogger<DeleteController> _logger;

        public DeleteController(DeleteContactService deleteContact, ILogger<DeleteController> logger)
        {
            _logger = logger;
            _deleteContact = deleteContact;
        }

        [HttpDelete("{Id_contacto}")]
        public async Task<IActionResult> DeleteContact(int Id_contacto)
        {
            try
            {
                // Intenta eliminar el contacto
                var deleteResult = await _deleteContact.DeleteContactByIdAsync(Id_contacto);

                // Verifica si el contacto fue eliminado o no encontrado
                if (!deleteResult)
                {
                    // Si no se encontró el contacto, retornar 404 Not Found
                    return NotFound(new { message = "El contacto no fue encontrado." });
                }

                return Ok(new { message = "Contacto eliminado exitosamente." });  // 200 OK con mensaje

            }
            catch (Exception ex)
            {
                // Loguear el error
                _logger.LogError($"Error al eliminar el contacto.: {ex.Message}");

                // Retornar una respuesta de error 500 Internal Server Error
                return StatusCode(500, "Error interno en el servidor.");
            }
        }

    }
}
