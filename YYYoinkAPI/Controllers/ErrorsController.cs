using Microsoft.AspNetCore.Mvc;

namespace YYYoinkAPI.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}