namespace YYYoinkAPI.Models;

public class AuthNCredentials
{
    public string Uuid { get; }
    public string Email { get; }
    public string RefreshToken { get; }
    public string AccessToken { get; }

    public AuthNCredentials(string uuid, string email, string refreshToken, string accessToken)
    {
        Uuid = uuid;
        Email = email;
        RefreshToken = refreshToken;
        AccessToken = accessToken;
    }

    public static AuthNCredentials? Create(
        string uuid,
        string email,
        string refreshToken,
        string accessToken
    )
    {
        return new AuthNCredentials(
            uuid,
            email,
            refreshToken,
            accessToken
        );
    }
}