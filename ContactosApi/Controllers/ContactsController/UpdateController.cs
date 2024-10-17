using ContactosApi.Services.contact;
using ContactosApi.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class UpdateController : ControllerBase
    {
        private readonly UpdateContactService _updateContact;
        private readonly ILogger<UpdateController> _logger;

        public UpdateController(UpdateContactService updateContact, ILogger<UpdateController> logger)
        {
            _updateContact = updateContact;
            _logger = logger;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactModel contact)
        {
            try
            {
                var updateResult = await _updateContact.UpdateContactAsync(id ,contact);

                if (!updateResult)
                {
                    return NotFound(new { message = "No se encontró el contacto para actualizar." });
                }

                return Ok(new { message = "Contacto actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el contacto: {ex.Message}");
                return StatusCode(500, new { message = "Error interno del servidor." });
            }
        }
    }
}
