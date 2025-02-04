namespace YYYoinkAPI.Models;

public class AuthNCredentials
{
    public string Email { get; }
    public string AccessToken { get; }
    public Guid RefreshToken { get; }

    public AuthNCredentials(string email, string accessToken, Guid refreshToken)
    {
        Email = email;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static AuthNCredentials? Create(
        string email,
        string accessToken,
        Guid refreshToken
    )
    {
        return new AuthNCredentials(
            email,
            accessToken,
            refreshToken
        );
    }
}