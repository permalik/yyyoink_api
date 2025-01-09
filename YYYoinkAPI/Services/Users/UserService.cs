using ErrorOr;
using YYYoinkAPI.Models;
using YYYoinkAPI.ServiceErrors;

namespace YYYoinkAPI.Services.Users;

public class UserService : IUserService
{
    private static readonly Dictionary<Guid, User> _users = new();
    public ErrorOr<Created> CreateUser(User user)
    {
        _users.Add(user.Id, user);

        return Result.Created;
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
        _users[user.Id] = user;

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        _users.Remove(id);

        return Result.Deleted;
    }
}