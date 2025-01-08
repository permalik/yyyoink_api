using Microsoft.AspNetCore.Mvc;
using YYYoinkAPI.Contracts.User;
using YYYoinkAPI.Models;

namespace YYYoinkAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        var user = new User(
            Guid.NewGuid(),
            request.Email,
            request.Password
        );

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
        return Ok(id);
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