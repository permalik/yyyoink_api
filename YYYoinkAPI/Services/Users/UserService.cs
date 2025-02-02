using ErrorOr;
using YYYoinkAPI.Logger;
using YYYoinkAPI.Models;
using YYYoinkAPI.ServiceErrors;
using YYYoinkAPI.Services.Postgres;
using ILogger = Serilog.ILogger;

namespace YYYoinkAPI.Services.Users;

public class UserService : IUserService
{
    private static readonly Dictionary<Guid, User> _users = new();

    public async Task<ErrorOr<Created>> CreateUser(User user)
    {
        // TODO: assert
        string? connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        Database db = new Database(connStr);
        ILogger log = new YYYLogger().Log;
        User createdUser = await db.CreateUserAsync(user);
        if (createdUser is null)
        {
            log.Error("{UserError} while executing {UserService}", nameof(UserErrors.NotFound), nameof(CreateUser));
            return UserErrors.NotFound;
        }

        return Result.Created;
    }

    public async Task<ErrorOr<User>> LoginUser(string email, string password)
    {
        // TODO: assert
        string? connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        Database db = new Database(connStr);
        ILogger log = new YYYLogger().Log;
        User user = await db.GetUserAsync(email);
        if (user is null)
        {
            log.Error("{UserError} while executing {UserService}", nameof(UserErrors.NotFound), nameof(LoginUser));
            return UserErrors.NotFound;
        }

        if (user.Password != password)
        {
            log.Error("{UserError} while executing {UserService}", nameof(UserErrors.Unauthorized), nameof(LoginUser));
            return UserErrors.Unauthorized;
        }

        return user;
    }

    public async Task<ErrorOr<User>> GetUser(Guid id)
    {
        string? connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        Database db = new Database(connStr);
        ILogger log = new YYYLogger().Log;
        User user = await db.GetUserByIdAsync(id);
        if (user is null)
        {
            log.Error("{UserError} while executing {UserService}", nameof(UserErrors.NotFound), nameof(GetUser));
            return UserErrors.NotFound;
        }

        return user;
    }

    public async Task<ErrorOr<Updated>> UpdateUser(Guid uuid, string email, string password)
    {
        string? connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        Database db = new Database(connStr);
        ILogger log = new YYYLogger().Log;
        User user = await db.UpdateUserAsync(uuid, email, password);
        if (user is null)
        {
            log.Error("{UserError} while executing {UserService}", nameof(UserErrors.NotFound), nameof(UpdateUser));
            return UserErrors.NotFound;
        }

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteUser(Guid id)
    {
        string? connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        Database db = new Database(connStr);
        ILogger log = new YYYLogger().Log;
        bool isDeleted = await db.DeleteUserAsync(id);
        if (isDeleted)
        {
            return Result.Deleted;
        }

        log.Error("{UserError} while executing {UserService}", nameof(UserErrors.NotFound), nameof(DeleteUser));
        return UserErrors.Failure;
    }
}