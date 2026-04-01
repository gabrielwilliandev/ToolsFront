namespace Tools.Domain.Entities
{
    public class User
    {
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        protected User() { }

        public User(string email, string passwordHash, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
