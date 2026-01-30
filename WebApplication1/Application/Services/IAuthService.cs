using static WebApplication1.Domain.Entities.UserEntite;

namespace WebApplication1.Application.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
