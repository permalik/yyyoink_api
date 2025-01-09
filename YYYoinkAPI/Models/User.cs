using ErrorOr;

namespace YYYoinkAPI.Models;

public class User
{
    public Guid Id { get; }
    public string Email { get; }
    public string Password { get; }

    public User(Guid id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }

    public static ErrorOr<User> Create(
        string email,
        string password
    )
    {
        return new User(
            Guid.NewGuid(),
            email,
            password
        );
    }
}