using ErrorOr;

namespace YYYoinkAPI.Models;

public class User
{
    public Guid Uuid { get; }
    public string Email { get; }
    public string Password { get; }

    public User(Guid uuid, string email, string password)
    {
        Uuid = uuid;
        Email = email;
        Password = password;
    }

    public static ErrorOr<User> Create(
        Guid uuid,
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