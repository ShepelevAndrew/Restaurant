using System.Security.Cryptography;
using System.Text;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Domain.Users;

public class User
{
    public static User Owner = new("name", "lastname", "owner@gmail.com", "+380698432576", "12345Te");

    static User()
    {
        Owner.ConfirmEmail();
        Owner.ChangeRole(Role.Owner.Id);
    }

    private const string Salt = "0ge3k4k6d90glkj32lk3jljv9";

    public User(string firstname, string lastname, string email, string phone, string password)
    {
        UserId = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Phone = phone;
        Password = Sha256Encoding(password, Salt);
        IsEmailConfirmed = false;
        RoleId = Role.User.Id;
    }

    // For EF Core
    private User()
    {
    }

    public Guid UserId { get; private set; } 

    public string Firstname { get; private set; } = string.Empty;

    public string Lastname { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string Phone { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;

    public bool IsEmailConfirmed { get; private set; }

    public int RoleId { get; private set; }

    public User ChangeRole(int roleId)
    {
        RoleId = roleId;

        return this;
    }

    public User Update(string? firstname, string? lastname, string? phone)
    {
        Firstname = firstname ?? Firstname;
        Lastname = lastname ?? Lastname;
        Phone = phone ?? Phone;

        return this;
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
    }

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