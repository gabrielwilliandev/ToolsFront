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
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("TNome é obrigatório.");
            }
            Name = name.Trim();
            Description = description?.Trim() ?? string.Empty;
        }


    }
}
