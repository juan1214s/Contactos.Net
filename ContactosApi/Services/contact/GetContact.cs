using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;

namespace ContactosApi.Services.Contact
{
    public class GetContact
    {
        private readonly Connection _connection; // Campo para almacenar la conexión

        // Constructor que recibe la conexión
        public GetContact(Connection connection)
        {
            // Inicializa el campo de conexión
            _connection = connection; 
        }

        public async Task<List<ContactModel>> GetContactsAsync()
        {
            var contacts = new List<ContactModel>();

            // Usa el método para obtener la cadena de conexión
            using (var conexion = new MySqlConnection(_connection.GetSqlConnection())) 
            {
                // Abre conexión asíncronamente
                await conexion.OpenAsync(); 

                using (var cmd = new MySqlCommand("sp_Listar", conexion))
                {
                    // Establece el tipo de comando para un procedimiento almacenado
                    cmd.CommandType = System.Data.CommandType.StoredProcedure; 

                    using (var reader = await cmd.ExecuteReaderAsync())
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

            // Devuelve la lista de contactos
            return contacts; 
        }
    }
}
