using ContactosApi.DataBase;
using ContactosApi.Models.User;
using MySql.Data.MySqlClient;
using System.Data;

namespace ContactosApi.Services.UserService
{
    public class GetUserByIdService
    {
        private readonly Connection _connection;
        private readonly ILogger<GetUserByIdService> _logger;

        public GetUserByIdService(Connection connection, ILogger<GetUserByIdService> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<GetUserModel?> GetUserById(int Id_user) // Cambiado a devolver un solo usuario
        {
            try
            {
                GetUserModel user = null;

                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    using (var command = new MySqlCommand("sp_ObtenerUsuarioPorId", conexion))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_userId", Id_user);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync()) // Cambiado a leer solo una vez
                            {
                                user = new GetUserModel
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    LastName = reader.GetString("LastName"),
                                    Email = reader.GetString("Email"),
                                    Password = reader.GetString("Password"),
                                };
                            }
                        }
                    }
                }

                return user; // Devuelve el usuario o null si no se encontró
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el usuario por su ID: {ex.Message}");
                return null; // Devuelve null en caso de error
            }
        }
    }
}
