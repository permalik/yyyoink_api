using ErrorOr;
using YYYoinkAPI.Models;

namespace YYYoinkAPI.Services.Users;

public interface IUserService
{
    Task<ErrorOr<Created>> CreateUser(User user);
    Task<ErrorOr<AuthNCredentials>> AuthN(string email, string password, string refreshToken);
    Task<ErrorOr<User>> GetUserById(Guid id);
    Task<ErrorOr<Updated>> UpdateUser(Guid uuid, string email, string password);
    Task<ErrorOr<Deleted>> DeleteUser(Guid id);
}