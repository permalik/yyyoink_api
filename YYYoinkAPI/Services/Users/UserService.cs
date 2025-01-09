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

    public void UpdateUser(User user)
    {
        _users[user.Id] = user;
    }

    public void DeleteUser(Guid id)
    {
        _users.Remove(id);
    }
}