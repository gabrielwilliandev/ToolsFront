using Tools.Domain.Exceptions;

namespace Tools.Domain.Entities
{
    public class Lista
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public ICollection<Tool> Tools { get; private set; } = new List<Tool>();

        protected Lista() { }
        public Lista(string name, Guid userId)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new DomainException("lista.nome.required", "Nome da lista é obrigatório.");

            Id = Guid.NewGuid();
            Name = name.Trim();
            UserId = userId;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("lista.nome.required", "Nome da lista é obrigatório.");
            Name = name.Trim();
        }
    }
}
