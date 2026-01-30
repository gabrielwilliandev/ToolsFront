namespace WebApplication1.Domain.Entities
{
    public class UserEntite
    {
        public record User(int Id, string Name, string Email, string Password, string[] Roles);
    }
}
