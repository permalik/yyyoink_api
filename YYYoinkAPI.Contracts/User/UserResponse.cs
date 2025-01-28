namespace YYYoinkAPI.Contracts.User;

public record UserResponse(
    Guid Uuid,
    string Email,
    string Password
);