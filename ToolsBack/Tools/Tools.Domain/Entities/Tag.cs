namespace Tools.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tool> Tools { get; set; } = new List<Tool>();

        public Tag() { }

        public Tag(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

    }
}
