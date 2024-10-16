using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ContactosApi.Services.contact
{
    public class GetByIdContact
    {
        private readonly Connection _connection;
        private readonly ILogger<GetByIdContact> _logger;

        public GetByIdContact(Connection connection, ILogger<GetByIdContact> logger)
        {
            _logger = logger;
            _connection = connection;
        }

        public async Task<List<ContactModel>> GetContactByIdAsync(int contactoId)
        {
            try
            {
                var contacts = new List<ContactModel>();

                // Abre la conexión usando el método OpenConnectionAsync()
                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    using (var cmd = new MySqlCommand("sp_ObtenerContactoId", conexion))
                    {
                        // Establece el tipo de comando para un procedimiento almacenado
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agrega los parámetros necesarios
                        cmd.Parameters.AddWithValue("contactoId", contactoId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            // Lee los resultados del procedimiento almacenado
                            while (await reader.ReadAsync())
                            {
                                var contact = new ContactModel
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    Phone = reader.GetString("Phone"),
                                    Email = reader.GetString("Email"),
                                    UserId = reader.GetInt32("UserId")
                                };

                                contacts.Add(contact);
                            }
                        }
                    }
                }

                // Devuelve la lista de contactos obtenidos
                return contacts;
            }
            catch (Exception ex)
            {
                // Registra el error utilizando el logger
                _logger.LogError($"Error al obtener el contacto por ID: {ex.Message}");
                return new List<ContactModel>(); // Devuelve una lista vacía en caso de error
            }
        }
    }
}
