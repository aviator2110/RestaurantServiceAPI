using Microsoft.AspNetCore.Identity;
using RestaurantServiceAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    public string Hash(string input)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            input,
            salt,
            Iterations,
            HashAlgorithmName.SHA256);

        var hash = pbkdf2.GetBytes(HashSize);

        var result = new byte[SaltSize + HashSize];

        Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
        Buffer.BlockCopy(hash, 0, result, SaltSize, HashSize);

        return Convert.ToBase64String(result);
    }

    public bool Verify(string input, string storedHash)
    {
        var hashBytes = Convert.FromBase64String(storedHash);

        var salt = new byte[SaltSize];
        var storedHashBytes = new byte[HashSize];

        Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);
        Buffer.BlockCopy(hashBytes, SaltSize, storedHashBytes, 0, HashSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            input,
            salt,
            Iterations,
            HashAlgorithmName.SHA256);

        var computedHash = pbkdf2.GetBytes(HashSize);

        return CryptographicOperations.FixedTimeEquals(storedHashBytes, computedHash);
    }
}