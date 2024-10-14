using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ContactosApi.Data
{
    public class Connection
    {
        private readonly string _cadenaSQL;

        public Connection()
        {
            // Obtener la cadena de conexión
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Asignar la cadena de conexión con un valor por defecto si es nula
            _cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value
                ?? throw new InvalidOperationException("La cadena de conexión no se pudo obtener.");
        }

        public string GetSqlConnection()
        {
            return _cadenaSQL;
        }
    }
}
