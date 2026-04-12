using System.Security.Cryptography;

namespace DV.Shared.Constants;

public static class DocumentTokenGenerator
{
    public static string GenerateToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(16);
        return Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
