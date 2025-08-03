using System.Security.Cryptography;
using System.Text;

namespace api_docmanager.Utils;

public class SecurityFunctions
{
    //Generates a 10 random number
    public static int GenerateRandomId()
    {
        var rng = new Random();
        return rng.Next(100_000_000, 1_000_000_000);
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}