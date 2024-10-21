using ContactosApi.Models.Contact;
using ContactosApi.Models.Seeker;
using ContactosApi.Services.SeekerService;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.SeekerController
{
    [ApiController]
    [Route("api/seeker")]
    public class SeekerController : ControllerBase
    {
        private readonly SeekerServices _seekerService;
        private readonly ILogger<SeekerController> _logger;

        public SeekerController(SeekerServices seekerService, ILogger<SeekerController> logger)
        {
            _seekerService = seekerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Seeker([FromQuery] string name, [FromQuery] int userId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("El nombre es requerido en la consulta.");
            }

            var seekerModel = new SeekerModel { Name = name, UserId = userId };

            try
            {
                var contacts = await _seekerService.Seeker(seekerModel);
                if (contacts == null || contacts.Count == 0) // Verifica si la lista está vacía
                {
                    return NotFound("No se encontraron contactos.");
                }

                return Ok(contacts); // Devuelve la lista de contactos encontrados
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar los contactos: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }


    }
}
