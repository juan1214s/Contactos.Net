using ContactosApi.Services.contact;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class GetContactByIdController : ControllerBase
    {

        private readonly GetByIdContactService _getByIdcontact;
        private readonly ILogger<GetContactByIdController> _logger;

        public GetContactByIdController(GetByIdContactService getByIdContact, ILogger<GetContactByIdController> logger)
        {
            _logger = logger;
            _getByIdcontact = getByIdContact;
        }

        [HttpGet("getById{contactoId}")]
        public async Task<IActionResult> GetContactId(int contactoId )
        {
            try
            {
                var getcontact = await _getByIdcontact.GetContactByIdAsync(contactoId);

                if (getcontact == null)
                {
                    _logger.LogWarning("No se encontro ningun contacto con ese ID.");
                    return NotFound("No se encontro un contacto con ese ID.");
                }

                return Ok(getcontact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al buscar un contacto por su ID: {ex.Message}");
            }
        
        }
    }
}
