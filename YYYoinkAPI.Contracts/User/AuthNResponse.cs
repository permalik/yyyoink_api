namespace YYYoinkAPI.Contracts.User;

public record AuthNResponse(
    string Email,
    string AccessToken,
    Guid RefreshToken
);