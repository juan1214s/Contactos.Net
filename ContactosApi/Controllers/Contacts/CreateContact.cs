using ContactosApi.Models.Contact;
using ContactosApi.Services.contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class CreateContact : ControllerBase
    {
        private readonly Services.contact.CreateContact _createContact;
        private readonly ILogger<CreateContact> _logger; 

        public CreateContact(Services.contact.CreateContact createContact, ILogger<CreateContact> logger)
        {
            _createContact = createContact;
            _logger = logger;
        }

        [HttpPost]
        //con FromBody especifico q coja los datos json del request
        public async Task<IActionResult> createContact([FromBody] ContactModel contact)
        {
            try
            {
                // Validar los datos del modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Llama al servicio para crear el contacto
                var createContact = await _createContact.CreateContacts(contact);

                // Retorna el contacto creado si todo va bien
                return Ok(createContact);
            }
            catch (Exception ex)
            {
                // Registra el error antes de devolver la respuesta
                _logger.LogError(ex, "Error al crear el contacto");
                return StatusCode(500, $"Error al crear el contacto: {ex.Message} \n StackTrace: {ex.StackTrace}");
            }
        }
    }
}
