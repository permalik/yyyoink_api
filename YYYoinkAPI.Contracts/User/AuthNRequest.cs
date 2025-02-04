namespace YYYoinkAPI.Contracts.User;

public record AuthNRequest(
    string Email,
    string Password,
    string? RefreshToken
);