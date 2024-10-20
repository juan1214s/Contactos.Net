using Microsoft.AspNetCore.Mvc;
using ContactosApi.Models.User;
using ContactosApi.Services.UserService;
using System.Threading.Tasks;

namespace ContactosApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UpdateUserService _updateUserService;

        public UsersController(UpdateUserService updateUserService)
        {
            _updateUserService = updateUserService;
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest("El modelo de usuario no puede ser nulo.");
            }

            var result = await _updateUserService.UpdateUser(id, userModel);

            if (!result)
            {
                return NotFound($"No se encontró el usuario con ID: {id}.");
            }

            return Ok(new { Message = $"Usuario actualizado exitosamente." });
        }
    }
}
