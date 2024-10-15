using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;

namespace ContactosApi.Services.contact
{
    public class UpdateContact
    {
        private readonly Connection _connection;
        private readonly ILogger<UpdateContact> _logger;
        private readonly GetByIdContact _getContact;

        public UpdateContact(Connection connection, ILogger<UpdateContact> logger, GetByIdContact getByIdContact)
        {
            _connection = connection;
            _logger = logger;
            _getContact = getByIdContact;
        }

        public async Task<bool> UpdateContactAsync(ContactModel contact)
        {
            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    var existContacts = await  _getContact.GetContactByIdAsync(contact.Id ?? 0);
                    var existContact = existContacts.FirstOrDefault();

                    if (existContact == null)
                    {
                        _logger.LogWarning($"No se encontró el contacto con ID: {contact.Id}");
                        return false;
                    }

                    contact.Name = string.IsNullOrWhiteSpace(contact.Name) ? existContact.Name : contact.Name;
                    contact.Phone = string.IsNullOrWhiteSpace(contact.Phone) ? existContact.Phone : contact.Phone;
                    contact.Email = string.IsNullOrWhiteSpace(contact.Email) ? existContact.Email : contact.Email;
                    contact.UserId = contact.UserId == 0 ? existContact.UserId : contact.UserId;

                    using (var command = new MySqlCommand("sp_updateContacto", conexion))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_id", contact.Id);
                        command.Parameters.AddWithValue("@p_name", contact.Name);
                        command.Parameters.AddWithValue("@p_phone", contact.Phone);
                        command.Parameters.AddWithValue("@p_email", contact.Email);
                        command.Parameters.AddWithValue("@p_userId", contact.UserId);

                        await command.ExecuteNonQueryAsync();
                    }
                }

              
                // Registrar un mensaje de éxito y retornar true
                _logger.LogInformation("Contacto actualizado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                // Registrar el error con el logger
                _logger.LogError($"Error al actualizar el contacto: {ex.Message}");
                return false;
            }
        }
    }
}
