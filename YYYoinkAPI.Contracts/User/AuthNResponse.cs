namespace YYYoinkAPI.Contracts.User;

public record AuthNResponse(
    string Uuid,
    string Email,
    string RefreshToken,
    string AccessToken
);