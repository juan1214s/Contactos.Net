using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;

namespace ContactosApi.Services.contact
{
    public class UpdateContactService
    {
        private readonly Connection _connection;
        private readonly ILogger<UpdateContactService> _logger;
        private readonly GetByIdContactService _getContact;

        public UpdateContactService(Connection connection, ILogger<UpdateContactService> logger, GetByIdContactService getByIdContact)
        {
            _connection = connection;
            _logger = logger;
            _getContact = getByIdContact;
        }

        public async Task<bool> UpdateContactAsync(int id, UpdateContactModel updateContactModel)
        {
            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {
                    var existContacts = await _getContact.GetContactByIdAsync(id);
                    var existContact = existContacts.FirstOrDefault();

                    if (existContact == null)
                    {
                        _logger.LogWarning($"No se encontró el contacto con ID: {id}");
                        return false;
                    }

                    // Actualización de campos (si no se proveen valores, se mantienen los existentes)
                    updateContactModel.Name = string.IsNullOrWhiteSpace(updateContactModel.Name) ? existContact.Name : updateContactModel.Name;
                    updateContactModel.Phone = string.IsNullOrWhiteSpace(updateContactModel.Phone) ? existContact.Phone : updateContactModel.Phone;
                    updateContactModel.Email = string.IsNullOrWhiteSpace(updateContactModel.Email) ? existContact.Email : updateContactModel.Email;

                    using (var command = new MySqlCommand("sp_updateContacto", conexion))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Pasar los parámetros al procedimiento almacenado
                        command.Parameters.AddWithValue("@p_contactId", id);
                        command.Parameters.AddWithValue("@p_name", updateContactModel.Name);
                        command.Parameters.AddWithValue("@p_phone", updateContactModel.Phone);
                        command.Parameters.AddWithValue("@p_email", updateContactModel.Email);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                // Registrar un mensaje de éxito y retornar true
                _logger.LogInformation($"Contacto con ID {id} actualizado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                // Registrar el error con el logger
                _logger.LogError($"Error al actualizar el contacto con ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
