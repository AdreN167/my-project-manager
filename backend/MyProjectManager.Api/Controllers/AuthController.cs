using Microsoft.AspNetCore.Mvc;
using MyProjectManager.Domain.Dto;
using MyProjectManager.Domain.Dto.User;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody]RegisterUserDto dto)
    {
        var response = await _authService.Register(dto);
        if(response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody]LoginUserDto dto)
    {
        var response = await _authService.Login(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
