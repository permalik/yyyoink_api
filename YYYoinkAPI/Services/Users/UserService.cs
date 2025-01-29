using ErrorOr;
using YYYoinkAPI.Models;
using YYYoinkAPI.ServiceErrors;
using YYYoinkAPI.Services.Postgres;

namespace YYYoinkAPI.Services.Users;

public class UserService : IUserService
{
    private static readonly Dictionary<Guid, User> _users = new();

    public ErrorOr<Created> CreateUser(User user)
    {
        _users.Add(user.Uuid, user);

        return Result.Created;
    }

    public async Task<ErrorOr<User>> LoginUser(string email, string password)
    {
        var connStr = Environment.GetEnvironmentVariable("PG_CS");
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

    public ErrorOr<User> GetUser(Guid id)
    {
        if (_users.TryGetValue(id, out var user))
        {
            return user;
        }

        return UserErrors.NotFound;
    }

    public ErrorOr<Updated> UpdateUser(User user)
    {
        if (_users.TryGetValue(user.Uuid, out var currentUser))
        {
            _users[user.Uuid] = user;
        }

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        _users.Remove(id);

        return Result.Deleted;
    }
}