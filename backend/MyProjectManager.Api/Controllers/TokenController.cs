using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProjectManager.Application.Services;
using MyProjectManager.Domain.Dto;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Api.Controllers;

[ApiController]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto dto)
    {
        var response = await _tokenService.RefreshToken(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

}
