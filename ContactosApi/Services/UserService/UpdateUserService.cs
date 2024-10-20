using ContactosApi.Data;
using ContactosApi.Models.User;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

namespace ContactosApi.Services.UserService
{
    public class UpdateUserService
    {
        private readonly Connection _connection;
        private readonly ILogger<UpdateUserService> _logger;
        private readonly GetUserByIdService _getUserByIdService;

        public UpdateUserService(Connection connection, ILogger<UpdateUserService> logger, GetUserByIdService getUserByIdService)
        {
            _connection = connection;
            _logger = logger;
            _getUserByIdService = getUserByIdService;
        }

        public async Task<bool> UpdateUser(int id, UserModel userModel)
        {
            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    // Llama a GetUserById para obtener el usuario existente
                    var existUser = await _getUserByIdService.GetUserById(id);

                    if (existUser == null)
                    {
                        _logger.LogWarning($"No se encontró el usuario con ID: {id}");
                        return false; // Usuario no encontrado
                    }

                    // Crea el comando para el procedimiento almacenado
                    using (var command = new MySqlCommand("sp_updateUsuario", conexion))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Establece los parámetros, utilizando los existentes si los nuevos son nulos o vacíos
                        command.Parameters.AddWithValue("@p_userId", id);
                        command.Parameters.AddWithValue("@p_name", string.IsNullOrWhiteSpace(userModel.Name) ? existUser.Name : userModel.Name);
                        command.Parameters.AddWithValue("@p_lastName", string.IsNullOrWhiteSpace(userModel.LastName) ? existUser.LastName : userModel.LastName);
                        command.Parameters.AddWithValue("@p_email", string.IsNullOrWhiteSpace(userModel.Email) ? existUser.Email : userModel.Email);
                        command.Parameters.AddWithValue("@p_password", string.IsNullOrWhiteSpace(userModel.Password) ? existUser.Password : userModel.Password);

                        // Ejecuta el comando
                        await command.ExecuteNonQueryAsync();
                    }
                }

                _logger.LogInformation($"Usuario con ID {id} actualizado exitosamente.");
                return true; // Actualización exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario con ID {id}: {ex.Message}");
                return false; // Maneja el error y devuelve false
            }
        }
    }
}
