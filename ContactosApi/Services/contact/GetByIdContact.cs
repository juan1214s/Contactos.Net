using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;
using System.Data;

namespace ContactosApi.Services.contact
{
    public class GetByIdContact
    {
        private readonly Connection _connection;

        public GetByIdContact(Connection connection)
        {
            _connection = connection;
        }

        public async Task<List<ContactModel>> GetContactByIdAsync(int contactoId)
        {
            var contacts = new List<ContactModel>();

            // Usa el método para obtener la cadena de conexión
            using (var conexion = new MySqlConnection(_connection.GetSqlConnection()))
            {
                // Abre conexión asíncronamente
                await conexion.OpenAsync();

                using (var cmd = new MySqlCommand("sp_ObtenerContactoId", conexion))
                {
                    // Establece el tipo de comando para un procedimiento almacenado
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@contactoId", contactoId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var contact = new ContactModel
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Phone = reader.GetString("Phone"), // Lo mismo aquí
                                Email = reader.GetString("Email"), // Y aquí
                                UserId = reader.GetInt32("UserId") // Verifica "UserId"
                            };


                            contacts.Add(contact);
                        }
                    }
                }
            }

            // Devuelve la lista de contactos
            return contacts;
        }
    }
}
