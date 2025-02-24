using Core.Interfaces;
using System.Security.Cryptography;

namespace Application.Utilities
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public string Hashing(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}