using MyProjectManager.Domain.Dto;
using MyProjectManager.Domain.Result;
using System.Security.Claims;

namespace MyProjectManager.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}
