using ContactosApi.DataBase;
using ContactosApi.Models.Contact;
using ContactosApi.Models.Seeker;
using MySql.Data.MySqlClient;
using System.Data;

namespace ContactosApi.Services.SeekerService
{
    public class SeekerServices
    {
        private readonly Connection _connection;
        private readonly ILogger<SeekerServices> _logger;

        public SeekerServices(Connection connection, ILogger<SeekerServices> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<List<ContactModel>> Seeker(SeekerModel seeker)
        {
            var contacts = new List<ContactModel>(); // Lista para almacenar los contactos encontrados
            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    using (var command = new MySqlCommand("sp_buscarContactos", conexion))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar los parámetros al procedimiento almacenado
                        command.Parameters.AddWithValue("@nombreParcial", seeker.Name);
                        command.Parameters.AddWithValue("@identificador", seeker.UserId);

                        // Ejecutar la consulta y obtener el resultado
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) // Cambiado a un ciclo while para leer múltiples filas
                            {
                                // Mapear los datos obtenidos al modelo ContactModel
                                var contact = new ContactModel
                                {
                                    Id = reader.GetInt32("id"),
                                    Name = reader.GetString("name"),
                                    Email = reader.GetString("email"),
                                    Phone = reader.GetString("phone"),
                                    UserId = reader.GetInt32("userId")
                                };

                                contacts.Add(contact); // Agregar el contacto a la lista
                            }
                        }
                    }
                }

                return contacts; // Devolver la lista de contactos
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los contactos: {ex.Message}");
                return new List<ContactModel>(); // Devolver una lista vacía en caso de error
            }
        }

    }
}
