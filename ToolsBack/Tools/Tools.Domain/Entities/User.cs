using Tools.Domain.Exceptions;

namespace Tools.Domain.Entities
{
    public class User
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public ICollection<Lista> Listas { get; private set; } = new List<Lista>();

        protected User() { }

        public User(string email, string passwordHash, string name)
        {

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("user.email.required", "Email é obrigatório.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("user.password.required", "Senha é obrigatória.");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("user.name.required", "Nome é obrigatório.");
            Id = Guid.NewGuid();
            Name = name;
            Email = email.Trim().ToLower();
            PasswordHash = passwordHash;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("user.name.required", "Nome é obrigatório.");

            Name = name.Trim();
        }
    }
}
