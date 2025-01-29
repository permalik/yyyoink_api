using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using YYYoinkAPI.Contracts.User;
using YYYoinkAPI.Models;
using YYYoinkAPI.Services.Users;

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
    public IActionResult CreateUser(CreateUserRequest request)
    {
        var user = new User(
            Guid.NewGuid(),
            request.Email,
            request.Password
        );

        ErrorOr<Created> createUserResult = _userService.CreateUser(user);

        return createUserResult.Match(
            created => CreatedAtGetUser(user),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        Task<ErrorOr<User>> loginUserResult = _userService.LoginUser(request.Email, request.Password);

        return loginUserResult.Match(
            loggedIn => Ok(MapUserResponse(loggedIn)),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetUser(Guid id)
    {
        ErrorOr<User> getUserResult = _userService.GetUser(id);

        return getUserResult.Match(
            user => Ok(MapUserResponse(user)),
            errors => Problem(errors)
        );
    }

    [HttpPatch("{id:guid}")]
    public IActionResult UpdateUser(Guid id, UpdateUserRequest request)
    {
        var user = new User(
            id,
            request.Email,
            request.Password
        );

        ErrorOr<Updated> updatedUserResult = _userService.UpdateUser(user);

        return updatedUserResult.Match(
            updated => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteUser(Guid id)
    {
        ErrorOr<Deleted> deleteUserResult = _userService.DeleteUser(id);

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
            actionName: nameof(GetUser),
            routeValues: new { uuid = user.Uuid },
            value: MapUserResponse(user)
        );
    }
}