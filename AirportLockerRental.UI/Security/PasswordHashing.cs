using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AirportLockerRental.UI.DataStorage;

namespace AirportLockerRental.UI.Security
{
    public class PasswordHashing
    {
        private const int _PBKDF2_ITERATIONS = 100000;
        private const int _SALT_SIZE = 16;
        private const int _HASH_SIZE = 32;
        public byte[] CreateSalt()
        {
            return RandomNumberGenerator.GetBytes(_SALT_SIZE);
        }

        public string HashPassword(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                _PBKDF2_ITERATIONS,
                HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(_HASH_SIZE);
            return Convert.ToHexString(hash);
        }
        public string? VerifyPassword(string password, User validationTarget)
        {
            byte[] salt = Convert.FromHexString(validationTarget.Salt);
            string hash = HashPassword(password, salt);
            if(hash == validationTarget.PasswordHash)
            {
                return hash;
            }
            else
            {
                return null;
            }
        }
    }
}
