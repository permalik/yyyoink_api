using ErrorOr;

namespace YYYoinkAPI.ServiceErrors;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "user not found"
    );
}