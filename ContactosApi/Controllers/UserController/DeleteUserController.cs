using ContactosApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace ContactosApi.Controllers.UserController
{
    [ApiController]
    [Route("api/users")]
    public class DeleteUserController : ControllerBase
    {
        private readonly DeleteUserService _deleteUserService;
        private readonly ILogger<DeleteUserController> _logger;

        public DeleteUserController(DeleteUserService deleteUserService, ILogger<DeleteUserController> logger)
        {
            _deleteUserService = deleteUserService;
            _logger = logger;
        }

        [HttpDelete("{Id_user}")]
        public async Task<IActionResult> DeleteUser(int Id_user)
        {
            try
            {
                var deleteUser = await _deleteUserService.DeleteUser(Id_user);

                if (!deleteUser)
                {
                    return NotFound(new { message = "Usuario no encontrado."});
                }

                return Ok(new { message = "Usuario eliminado exitosamente." }); 

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error al intentar eliminar el usuario: {ex.Message}");
                return StatusCode(500, "Error interno al intentar eliminar el usuario.");
            }
        }

    }
}

