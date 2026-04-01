using BCrypt.Net;
using Tools.Application.DTOs.Auth;
using Tools.Application.Interfaces;
using Tools.Application.Notifications;
using Tools.Domain.Entities;

namespace Tools.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly TokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly NotificationContext _notificationContext;

        public AuthService(TokenService tokenService, IUserRepository userRepository, NotificationContext notificationContext)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _notificationContext = notificationContext;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null)
            {
                _notificationContext.AddErrors("user.error", "Usuário ou senha inválidos");
                return null;
            }


            var validPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!validPassword)
            {
                _notificationContext.AddErrors("user.error", "Usuário ou senha inválidos");
                return null;
            }

            return _tokenService.GenerateToken(user);

        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);

            if(registerDto.Password != registerDto.ConfirmPass)
            {
                _notificationContext.AddErrors("password.error", "As senhas não coincidem");
                return;
            }

            if (existingUser != null)
            {
                _notificationContext.AddErrors("user.error", "Email já cadastrado");
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User(registerDto.Email, hashedPassword, registerDto.Nome);
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
