namespace WebApplication1.Domain.Entities
{
    public class TagEntitie
    {
        public int TagId { get; set; }
        public required string Name { get; set; }

        public ICollection<ToolTag> ToolTags { get; set; } = new List<ToolTag>();
    }
}
