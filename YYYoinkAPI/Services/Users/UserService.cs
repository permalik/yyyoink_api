using YYYoinkAPI.Models;

namespace YYYoinkAPI.Services.Users;

public class UserService : IUserService
{
    private static readonly Dictionary<Guid, User> _users = new();
    public void CreateUser(User user)
    {
        _users.Add(user.Id, user);
    }

    public User GetUser(Guid id)
    {
        return _users[id];
    }
}