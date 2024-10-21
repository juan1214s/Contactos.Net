using ContactosApi.DataBase;
using ContactosApi.Models.Contact;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace ContactosApi.Services.contact
{
    public class DeleteContactService
    {
        private readonly Connection _connection;
        private readonly ILogger<DeleteContactService> _logger;

        public DeleteContactService(Connection connection, ILogger<DeleteContactService> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<bool> DeleteContactByIdAsync(int Id_contacto)
        {
            try
            {
                // Usa el método para obtener la conexión
                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    using (var cmd = new MySqlCommand("sp_EliminarContacto", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar el parámetro necesario para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@Id_contacto", Id_contacto);

                        // Ejecutar el procedimiento almacenado
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                // Registrar un mensaje de éxito y retornar true
                _logger.LogInformation("Contacto eliminado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                // Registrar el error con el logger
                _logger.LogError($"Error al eliminar el contacto: {ex.Message}");
                return false;
            }
        }
    }
}
