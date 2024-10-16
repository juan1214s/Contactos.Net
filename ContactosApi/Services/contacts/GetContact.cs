using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

namespace ContactosApi.Services.Contact
{
    public class GetContact
    {
        private readonly Connection _connection;
        private readonly ILogger<GetContact> _logger;

        //inyecto las dependencias 
        public GetContact(Connection connection, ILogger<GetContact> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<List<ContactModel>> GetContactsAsync(int p_userId)
        {
            var contacts = new List<ContactModel>();

            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {

                    using (var command = new MySqlCommand("sp_ObterContactos", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@p_userId", p_userId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
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

                _logger.LogInformation("Contactos obtenidos exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los contactos: {ex.Message}");
            }

            return contacts;
        }
    }
}
