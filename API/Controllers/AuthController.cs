using API.Services;
using CMS.Application.Users.Dtos;
using Domain.Login;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto model)
    {
        if (model.Username != "admin" || model.Password != "123")
            return Unauthorized();

        var token = _tokenService.CreateToken(model.Username);
        return Ok(new LoginResponse
        {
            Token = token,
            ExpiresIn = 3600
        });
    }

}
