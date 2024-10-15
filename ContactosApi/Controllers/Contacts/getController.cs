using Microsoft.AspNetCore.Mvc;
using ContactosApi.Models.Contact;
using ContactosApi.Services.contact;
using ContactosApi.Services.Contact;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly GetContact _getContact; // Cambia esto según tu lógica de obtención de contactos

        public ContactsController(GetContact getContact)
        {
            _getContact = getContact;
        }

        [HttpGet("list/{p_userId}")]
        public async Task<IActionResult> GetContacts(int p_userId)
        {
            try
            {
                // Llama al servicio que utiliza el procedimiento almacenado
                var contacts = await _getContact.GetContactsAsync(p_userId);

                if (contacts == null || !contacts.Any())
                {
                    return NotFound("No se encontraron contactos.");
                }

                // Devuelve los contactos en formato JSON
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
