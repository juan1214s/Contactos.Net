﻿using ContactosApi.Data;
using ContactosApi.Models.Contact;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ContactosApi.Services.contact
{
    public class CreateContact
    {
        private readonly Connection _connection;
        private readonly ILogger<CreateContact> _logger;

        public CreateContact(Connection connection, ILogger<CreateContact> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<bool> CreateContacts(ContactModel contact)
        {
            try
            {
                using (var conexion = await _connection.OpenConnectionAsync())
                {

                    using (var cmd = new MySqlCommand("sp_createContacto", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agregar los parámetros necesarios para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@p_name", contact.Name);
                        cmd.Parameters.AddWithValue("@p_phone", contact.Phone);
                        cmd.Parameters.AddWithValue("@p_email", contact.Email);
                        cmd.Parameters.AddWithValue("@p_userId", contact.UserId);

                        // Ejecutar el procedimiento almacenado de forma asíncrona
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                // Registrar un mensaje de éxito y retornar true
                _logger.LogInformation("Contacto creado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                // Registra el error utilizando el logger
                _logger.LogError($"Error al obtener el contacto por ID: {ex.Message}");
                return false;
            }
        }
    }
}
