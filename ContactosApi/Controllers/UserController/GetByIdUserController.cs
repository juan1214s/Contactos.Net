using ContactosApi.Models.User;
using ContactosApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.UserController
{
    [ApiController]
    [Route("api/user")]
    public class GetByIdUserController : ControllerBase
    {
        private readonly GetUserByIdService _getUserByIdService;
        private readonly ILogger<GetByIdUserController> _logger;

        public GetByIdUserController(GetUserByIdService getUserByIdService, ILogger<GetByIdUserController> logger)
        {
            _getUserByIdService = getUserByIdService;
            _logger = logger;
        }

        [HttpGet("{Id_user}")]
        public async Task<IActionResult> GetUserById(int Id_user)
        {
            try
            {
                var user = await _getUserByIdService.GetUserById(Id_user); // Cambiado a obtener un solo usuario

                if (user == null) // Verifica si el usuario es null
                {
                    _logger.LogInformation($"No se encontró el usuario con ID: {Id_user}");
                    return NotFound(new { message = "El usuario no ha sido encontrado." });
                }

                return Ok(user); // Retorna el usuario encontrado
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error interno del servidor al buscar un usuario por su ID: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
