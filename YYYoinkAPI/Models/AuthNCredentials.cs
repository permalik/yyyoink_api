namespace YYYoinkAPI.Models;

public class AuthNCredentials
{
    public string Email { get; }
    public string Uuid { get; }
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public AuthNCredentials(string email, string uuid, string accessToken, string refreshToken)
    {
        Email = email;
        Uuid = Uuid;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static AuthNCredentials? Create(
        string email,
        string uuid,
        string accessToken,
        string refreshToken
    )
    {
        return new AuthNCredentials(
            email,
            uuid,
            accessToken,
            refreshToken
        );
    }
}