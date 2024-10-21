using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using ContactosApi.DataBase;

namespace ContactosApi.Services.Contact
{
    public class GetContactService
    {
        private readonly Connection _connection;
        private readonly ILogger<GetContactService> _logger;

        //inyecto las dependencias 
        public GetContactService(Connection connection, ILogger<GetContactService> logger)
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
