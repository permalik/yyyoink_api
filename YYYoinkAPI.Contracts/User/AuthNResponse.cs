namespace YYYoinkAPI.Contracts.User;

public record AuthNResponse(
    string Email,
    string Uuid,
    string AccessToken,
    string RefreshToken
);