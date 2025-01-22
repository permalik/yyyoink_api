using ErrorOr;

namespace YYYoinkAPI.ServiceErrors;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        code: "User.NotFound",
        description: "user not found"
    );

    public static Error Unauthorized => Error.Unauthorized ( 
        code: "User.Unauthorized",
        description: "user unauthorized"
    );
}