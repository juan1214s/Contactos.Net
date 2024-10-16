using ContactosApi.Services.contact;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.Contacts
{
    [ApiController]
    [Route("api/contacts")]
    public class GetContactById : ControllerBase
    {

        private readonly GetByIdContact _getByIdcontact;
        private readonly ILogger<GetContactById> _logger;

        public GetContactById(GetByIdContact getByIdContact, ILogger<GetContactById> logger)
        {
            _logger = logger;
            _getByIdcontact = getByIdContact;
        }

        [HttpGet("getById{contactoId}")]
        public async Task<IActionResult> GetContactId(int contactoId )
        {
            try
            {
                var contact = await _getByIdcontact.GetContactByIdAsync(contactoId);

                if (contact == null)
                {
                    _logger.LogWarning("No se encontro ningun contacto con ese ID.");
                    return NotFound("No se encontro un contacto con ese ID.");
                }

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor al buscar un contacto por su ID: {ex.Message}");
            }
        
        }
    }
}
