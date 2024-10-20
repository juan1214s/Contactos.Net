using ContactosApi.Models.User;
using ContactosApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactosApi.Controllers.UserController
{
    [ApiController]
    [Route("api/users")]
    public class CreateUserController : ControllerBase
    {
        private readonly CreateUserService _createUserService;
        private readonly ILogger<CreateUserController> _logger;

        public CreateUserController(CreateUserService createUserService, ILogger<CreateUserController> logger)
        {
            _createUserService = createUserService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel userModel)
        {
            try
            {
                var createUserResult = await _createUserService.CreateUser(userModel);

                if (createUserResult)
                {
                    return Ok(new { message = "User created successfully" });
                }
                else
                {
                    return BadRequest(new { message = "User creation failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a user.");
                return StatusCode(500, new { message = "An error occurred while creating the user." });
            }
        }
    }
}
