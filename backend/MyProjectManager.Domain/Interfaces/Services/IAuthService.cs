using MyProjectManager.Domain.Dto;
using MyProjectManager.Domain.Dto.User;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}
