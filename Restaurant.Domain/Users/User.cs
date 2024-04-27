using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Domain.Users;

public class User
{
    private const string Salt = "0ge3k4k6d90glkj32lk3jljv9";

    public User(string firstname, string lastname, string email, string phone, string password)
    {
        UserId = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Phone = phone;
        Password = Sha256Encoding(password, Salt);
    }

    // For EF Core
    private User()
    {
        Firstname = string.Empty;
        Lastname = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Password = string.Empty;
    }

    public Guid UserId { get; private set; }

    public string Firstname { get; private set; }

    public string Lastname { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public string Password { get; private set; }

    public bool ComparePassword(string password)
    {
        var hashPassword = Sha256Encoding(password, Salt);

        return Password.Equals(hashPassword);
    }

    private static string Sha256Encoding(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password + salt);
        var hashBytes = sha256.ComputeHash(inputBytes);

        var builder = new StringBuilder();
        foreach (var b in hashBytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}