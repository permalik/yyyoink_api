using ErrorOr;
using Serilog;
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
        string connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        var db = new Database(connStr);
        var createdUser = await db.CreateUserAsync(user);
        if (createdUser is null)
        {
            // log.Error("{UserError} while executing {ServiceName}", nameof(UserErrors.NotFound), nameof(CreateUser));
            return UserErrors.NotFound;
        }

        Console.WriteLine($"{user.Email} has been created");
        return Result.Created;
    }

    public async Task<ErrorOr<User>> LoginUser(string email, string password)
    {
        // TODO: assert
        var connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        var db = new Database(connStr);
        var user = await db.GetUserAsync(email);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        if (user.Password != password)
        {
            return UserErrors.Unauthorized;
        }

        Console.WriteLine($"{user.Email} has been logged in");
        return user;
    }

    public async Task<ErrorOr<User>> GetUser(Guid id)
    {
    ILogger log = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("./logs/log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
    
        var connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        var db = new Database(connStr);
        var user = await db.GetUserByIdAsync(id);
        log.Information("{UserError} while executing {UserService}", nameof(UserErrors.NotFound), nameof(GetUser));
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        return user;
    }

    public async Task<ErrorOr<Updated>> UpdateUser(Guid uuid, string email, string password)
    {
        var connStr = Environment.GetEnvironmentVariable("PG_CS") ?? string.Empty;
        var db = new Database(connStr);
        var user = await db.UpdateUserAsync(uuid, email, password);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        _users.Remove(id);

        return Result.Deleted;
    }
}