using ContactosApi.DataBase;
using MySql.Data.MySqlClient;

namespace ContactosApi.Services.UserService
{
    public class DeleteUserService
    {
        private readonly Connection _connection;
        private readonly ILogger<DeleteUserService> _logger;

        public DeleteUserService(Connection connection, ILogger<DeleteUserService> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<bool> DeleteUser(int id_usuario)
        {
            try
            {
                using(var conexion = await _connection.OpenConnectionAsync())
                {
                    using (var command = new MySqlCommand("sp_deleteUser", conexion))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_userId", id_usuario);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                _logger.LogInformation("Usuario eliminado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el usuario: {ex.Message}");
                return false;
            }
        }
    }
}
