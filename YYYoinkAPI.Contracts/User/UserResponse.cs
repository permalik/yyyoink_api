namespace YYYoinkAPI.Contracts.User;

public record UserResponse(
    Guid Id,
    string Email,
    string Password
);