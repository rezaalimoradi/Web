using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    [HttpGet("secret")]
    public IActionResult GetSecret()
    {
        var user = User.Identity?.Name;
        return Ok(new { message = $"Hello {user}, this is protected." });
    }

    [HttpGet("another")]
    public IActionResult GetAnother()
    {
        var user = User.Identity?.Name;
        return Ok(new { message = $"Hello {user}, this is another protected data." });
    }

}
