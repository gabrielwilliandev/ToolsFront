using Tools.Application.DTOs.Auth;

namespace Tools.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto loginDto);
         Task RegisterAsync(RegisterDto registerDto);
    }
}
