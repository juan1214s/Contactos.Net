using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ContactosApi.DataBase
{
    public class Connection
    {
        private readonly string _cadenaSQL;

        public Connection()
        {
            // Obtener la cadena de conexión desde el archivo de configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Asignar la cadena de conexión con un valor por defecto si es nula
            _cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value
                ?? throw new InvalidOperationException("La cadena de conexión no se pudo obtener.");
        }

        // Método para abrir una conexión a la base de datos
        public async Task<MySqlConnection> OpenConnectionAsync()
        {
            try
            {
                var conexion = new MySqlConnection(_cadenaSQL);
                await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona
                return conexion; // Devolver la conexión abierta
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al abrir la conexión con la base de datos.", ex);
            }
        }

        // Método para cerrar la conexión (opcional)
        public void CloseConnection(MySqlConnection conexion)
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
