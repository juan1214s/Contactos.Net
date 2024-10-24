using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace ContactosApi.Bcrypt 
{
    public class PasswordHasher
    {
        private readonly int _workFactor;

        public PasswordHasher(int workFactor = 12)
        {
            _workFactor = workFactor;
        }

        // Método para encriptar la contraseña
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        }

        // Método para verificar si la contraseña coincide con el hash
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
