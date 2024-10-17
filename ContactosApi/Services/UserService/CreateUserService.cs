using ContactosApi.Data;
using ContactosApi.Models.User;
using MySql.Data.MySqlClient;

namespace ContactosApi.Services.UserService
{
    public class CreateUserService
    {
        private readonly Connection _connection;
        private readonly ILogger<CreateUserService> _logger;

        public CreateUserService(Connection connection, ILogger<CreateUserService> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<bool> CreateUser(CreateUserModel createUserModel)
        {
            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {

                    using (var cmd = new MySqlCommand("sp_insertUser", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar los parámetros necesarios para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@p_name", createUserModel.Name);
                        cmd.Parameters.AddWithValue("@p_lastname", createUserModel.LastName);
                        cmd.Parameters.AddWithValue("@p_email", createUserModel.Email);
                        cmd.Parameters.AddWithValue("@p_password", createUserModel.Password);

                        // Ejecutar el procedimiento almacenado de forma asíncrona
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                // Registrar un mensaje de éxito y retornar true
                _logger.LogInformation("Usuario creado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                // Registra el error utilizando el logger
                _logger.LogError($"Error al obtener el usuario por ID: {ex.Message}");
                return false;
            }
        }
    }
}
