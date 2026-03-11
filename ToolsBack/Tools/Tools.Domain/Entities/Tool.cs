using Tools.Domain.Exceptions;

namespace Tools.Domain.Entities
{
    public class Tool
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        protected Tool() { }

        public Tool(string name, string description)
        {
            ValidateTool(name, description);

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            ValidateTool(name, description);

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("tool.name.required", "Nome é obrigatório.");
            }
            Name = name.Trim();
            Description = description?.Trim() ?? string.Empty;
        }

        private static void ValidateTool(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("tool.name.required", "Nome é obrigatório.");
            if (name.Length > 100)
                throw new DomainException("tool.name.maxLength", "Nome deve conter no máximo 100 caracteres.");
            if (description != null && description.Length > 2500)
                throw new DomainException("tool.description.maxLength", "Descrição deve conter no máximo 2500 caracteres.");


        }
    }
}
