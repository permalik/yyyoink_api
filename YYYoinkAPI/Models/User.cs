using ErrorOr;

namespace YYYoinkAPI.Models;

public class User
{
    public string Email { get; }
    public string Password { get; }
    public Guid Uuid { get; }
    public Guid RefreshToken { get; }

    public User(string email, string password, Guid uuid, Guid refreshToken)
    {
        Email = email;
        Password = password;
        Uuid = uuid;
        RefreshToken = refreshToken;
    }

    public static ErrorOr<User> Create(
        string email,
        string password,
        Guid uuid,
        Guid refreshToken
    )
    {
        return new User(
            email,
            password,
            Guid.NewGuid(),
            refreshToken
        );
    }
}