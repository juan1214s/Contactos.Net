using Microsoft.AspNetCore.Mvc;
using ContactosApi.Models.Contact;
using ContactosApi.Services.contact;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ContactosApi.Services.Contact;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly GetContactService _getContact;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(GetContactService getContact, ILogger<ContactsController> logger)
        {
            _getContact = getContact;
            _logger = logger;
        }

        [HttpGet("{p_userId}")]
        public async Task<IActionResult> GetContacts(int p_userId)
        {

            try
            {
                // Llama al servicio que utiliza el procedimiento almacenado
                var contacts = await _getContact.GetContactsAsync(p_userId);

                if (contacts == null || !contacts.Any())
                {
                    _logger.LogWarning("No se encontraron contactos para el usuario con ID: {p_userId}", p_userId);
                    return NotFound("No se encontraron contactos.");
                }

                _logger.LogInformation("Contactos obtenidos exitosamente para el usuario con ID: {p_userId}", p_userId);
                // Devuelve los contactos en formato JSON
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener contactos para el usuario con ID: {p_userId}", p_userId);
                // Manejo de errores
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
