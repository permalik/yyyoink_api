using Microsoft.AspNetCore.Mvc;
using YYYoinkAPI.Contracts.User;
using YYYoinkAPI.Models;
using YYYoinkAPI.Services.Users;

namespace YYYoinkAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
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

        _userService.CreateUser(user);

        var response = new UserResponse(
            user.Id,
            user.Email,
            user.Password
        );

        return CreatedAtAction(
            actionName: nameof(GetUser),
            routeValues: new { id = user.Id },
            value: response
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetUser(Guid id)
    {
        User user = _userService.GetUser(id);

        var response = new UserResponse(
            user.Id,
            user.Email,
            user.Password
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateUser(Guid id, UpdateUserRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteUser(Guid id)
    {
        return Ok(id);
    }
}