namespace Tools.Application.DTOs.Auth
{
    public class RegisterDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPass { get; set; } = string.Empty;
    }
}
