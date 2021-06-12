using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHashing
{
    public class PasswordHasher : IPasswordHasher
    {
        public int SaltSize { get; set; }
        public int Iterations { get; set; }
        public int KeySize { get; set; }
        public bool Check(string hash, string password)
        {
            SaltSize = 16; // Cantidad números aleatorios que se adjuntan al hash para aumentar seguridad. 128bit
            Iterations = 1000000; //Cantidad de veces que se repite la ejecución el algoritmo sobre el hash. 
            KeySize = 32; // 256bit

            var parts = hash.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Unexpected hash format");

            byte[] salt = Convert.FromBase64String(parts[1]);
            int iterations = Convert.ToInt32(parts[0]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
             password,
             salt,
             iterations,
             HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                return keyToCheck.SequenceEqual(key);
            }

        }

        public string Hash(string password)
        {
            SaltSize = 16; // Cantidad números aleatorios que se adjuntan al hash para aumentar seguridad. 128bit
            Iterations = 1000000; //Cantidad de veces que se repite la ejecución el algoritmo sobre el hash. 
            KeySize = 32; // 256bit
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                Iterations,
                HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String( algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Iterations}.{salt}.{key}";
            }

        }
    }
}
