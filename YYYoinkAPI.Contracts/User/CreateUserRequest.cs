namespace YYYoinkAPI.Contracts.User;

public record CreateUserRequest(
    string Email,
    string Password
);