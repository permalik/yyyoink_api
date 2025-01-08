using YYYoinkAPI.Models;

namespace YYYoinkAPI.Services.Users;

public interface IUserService
{
    void CreateUser(User user);
    User GetUser(Guid id);
}