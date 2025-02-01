using ErrorOr;
using YYYoinkAPI.Models;

namespace YYYoinkAPI.Services.Users;

public interface IUserService
{
    Task<ErrorOr<Created>> CreateUser(User user);
    Task<ErrorOr<User>> LoginUser(string email, string password);
    Task<ErrorOr<User>> GetUser(Guid id);
    Task<ErrorOr<Updated>> UpdateUser(Guid uuid, string email, string password);
    Task<ErrorOr<Deleted>> DeleteUser(Guid id);
}