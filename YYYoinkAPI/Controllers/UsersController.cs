using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using YYYoinkAPI.Contracts.User;
using YYYoinkAPI.Logger;
using YYYoinkAPI.Models;
using YYYoinkAPI.ServiceErrors;
using YYYoinkAPI.Services.AuthN;
using YYYoinkAPI.Services.Users;
using ILogger = Serilog.ILogger;

namespace YYYoinkAPI.Controllers;

public class UsersController : APIController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        // TODO: impl assertion
        if (userService == null)
        {
            throw new ArgumentNullException(nameof(userService), "userservice cannot be null");
        }

        _userService = userService;
    }

    [HttpPost]
    public Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        User user = new User(
            Guid.NewGuid(),
            request.Email,
            request.Password
        );
        Task<ErrorOr<Created>> createUserResult = _userService.CreateUser(user);
        return createUserResult.Match(
            created => CreatedAtGetUser(user),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        Task<ErrorOr<User>> loginUserResult = _userService.LoginUser(request.Email, request.Password, Response);
        return loginUserResult.Match(
            loggedIn => Ok(MapUserResponse(loggedIn)),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public Task<IActionResult> GetUserById(Guid id)
    {
        Task<ErrorOr<User>> getUserResult = _userService.GetUserById(id);
        return getUserResult.Match(
            user => Ok(MapUserResponse(user)),
            errors => Problem(errors)
        );
    }

    [HttpPatch("update/{id:guid}")]
    public Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
    {
        Task<ErrorOr<Updated>> updatedUserResult = _userService.UpdateUser(id, request.Email, request.Password);
        return updatedUserResult.Match(
            updated => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("delete/{id:guid}")]
    public Task<IActionResult> DeleteUser(Guid id)
    {
        Task<ErrorOr<Deleted>> deleteUserResult = _userService.DeleteUser(id);
        return deleteUserResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private static UserResponse MapUserResponse(User user)
    {
        return new UserResponse(
            user.Uuid,
            user.Email,
            user.Password
        );
    }
    
    private CreatedAtActionResult CreatedAtGetUser(User user)
    {
        return CreatedAtAction(
            actionName: nameof(CreateUser),
            routeValues: new { uuid = user.Uuid },
            value: MapUserResponse(user)
        );
    }
}