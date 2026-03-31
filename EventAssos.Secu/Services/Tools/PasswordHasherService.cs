using EventAssos.Secu.Interfaces.Services.Tools;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EventAssos.Secu.Services.Tools;

    internal class PasswordHasherService : IPasswordHasherService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 4;
        private const int MemorySize = 65536;
        private const int DegreeOfParallelism = 2;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = HashPasswordWithSalt(password, salt);

            byte[] combined = new byte[SaltSize + HashSize];

            Array.Copy(salt, 0, combined, 0, SaltSize);
            Array.Copy(hash, 0, combined, SaltSize, HashSize);

            return Convert.ToBase64String(combined);
        }

        private byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                Iterations = Iterations,
                MemorySize = MemorySize,
                DegreeOfParallelism = DegreeOfParallelism
            };

            return argon2.GetBytes(HashSize);
        }

        public bool VerifyPassword(string password, string storedPassword)
        {
            byte[] hashWithSalt = Convert.FromBase64String(storedPassword);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashWithSalt, 0, salt, 0, SaltSize);

            byte[] storedHash = new byte[HashSize];
            Array.Copy(hashWithSalt, SaltSize, storedHash, 0, HashSize);

            byte[] computedHash = HashPasswordWithSalt(password, salt);

            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }
    }

