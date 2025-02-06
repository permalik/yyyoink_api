using ErrorOr;

namespace YYYoinkAPI.Models;

public class User
{
    public Guid Uuid { get; }
    public string Email { get; }
    public string Password { get; }
    public Guid? RefreshToken { get; }

    public User(Guid uuid, string email, string password, Guid? refreshToken)
    {
        Uuid = uuid;
        Email = email;
        Password = password;
        RefreshToken = refreshToken;
    }

    public static ErrorOr<User> Create(
        Guid uuid,
        string email,
        string password,
        Guid refreshToken
    )
    {
        return new User(
            Guid.NewGuid(),
            email,
            password,
            refreshToken
        );
    }
}