using System.Security.Cryptography;
using System.Text;

namespace PersonManagement.Container
{
    public class EncryptionService
    {

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public string HashPasswordWithSalt(string password, string salt)
        {
            string saltedPassword = password + salt;
            return HashPassword(saltedPassword);
        }
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string hashedInput = HashPasswordWithSalt(enteredPassword, storedSalt);
            return hashedInput == storedHash;
        }
    }
}
