namespace YYYoinkAPI.Models;

public class AuthNCredentials
{
    public string Email { get; }
    public string Uuid { get; }
    public string RefreshToken { get; }
    public string AccessToken { get; }

    public AuthNCredentials(string email, string uuid, string refreshToken, string accessToken)
    {
        Email = email;
        Uuid = uuid;
        RefreshToken = refreshToken;
        AccessToken = accessToken;
    }

    public static AuthNCredentials? Create(
        string email,
        string uuid,
        string refreshToken,
        string accessToken
    )
    {
        return new AuthNCredentials(
            email,
            uuid,
            refreshToken,
            accessToken
        );
    }
}