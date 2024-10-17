using ContactosApi.Models.Contact;
using ContactosApi.Services.contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class CreateController : ControllerBase
    {
        private readonly CreateContactService _createContactService; 
        private readonly ILogger<CreateController> _logger;

        public CreateController(CreateContactService createContactService, ILogger<CreateController> logger)
        {
            _createContactService = createContactService;
            _logger = logger;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateContact(int userId  ,[FromBody] CreateContactModel createContactModel) 
        {
            try
            {
                // Validar los datos del modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Llama al servicio para crear el contacto
                var createdContact = await _createContactService.CreateContacts(userId, createContactModel);

                // Retorna el contacto creado si todo va bien
                return Ok(new {message =  "Se creo correctamente el contacto."});
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
