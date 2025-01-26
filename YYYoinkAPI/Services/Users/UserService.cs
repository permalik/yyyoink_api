using ErrorOr;
using YYYoinkAPI.Models;
using YYYoinkAPI.ServiceErrors;

namespace YYYoinkAPI.Services.Users;

public class UserService : IUserService
{
    private static readonly Dictionary<Guid, User> _users = new();
    public ErrorOr<Created> CreateUser(User user)
    {
        var accountProducer = new AccountProducerService();
        accountProducer.Produce(user.Email, user.Password);
        
        // _users.Add(user.Id, user);
        
        return Result.Created;
    }

    public ErrorOr<User> LoginUser(string email, string password)
    {
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
        _users[user.Id] = user;

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        _users.Remove(id);

        return Result.Deleted;
    }
}