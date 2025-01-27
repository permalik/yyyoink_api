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
        _users.Add(user.Id, user);
        
        return Result.Created;
    }

    public async Task<ErrorOr<User>> LoginUser(string email, string password)
    {
        const string connStr = "str";
        var db = new Database(connStr);
        var users = await db.GetUserAsync();
        foreach (var u in users)
        {
            Console.WriteLine($"user {u}");
        }
        
        var user = _users.Values.FirstOrDefault(u => u.Email == email);

        if (user is null)
        {
            return UserErrors.NotFound;
        }

        if (user.Password != password)
        {
            return UserErrors.Unauthorized;
        }

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
        if (_users.TryGetValue(user.Id, out var currentUser))
        {
            _users[user.Id] = user;
        }

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        _users.Remove(id);

        return Result.Deleted;
    }
}