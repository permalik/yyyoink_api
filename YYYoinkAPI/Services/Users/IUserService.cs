using ErrorOr;
using YYYoinkAPI.Models;

namespace YYYoinkAPI.Services.Users;

public interface IUserService
{
    ErrorOr<Created> CreateUser(User user);
    ErrorOr<User> GetUser(Guid id);
    ErrorOr<Updated> UpdateUser(User user);
    ErrorOr<Deleted> DeleteUser(Guid id);
}