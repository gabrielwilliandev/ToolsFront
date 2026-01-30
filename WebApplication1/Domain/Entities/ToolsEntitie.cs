namespace WebApplication1.Domain.Entities
{
    public class ToolsEntitie
    {
        public int ToolId { get; set; }
        public required string Title { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public ICollection<ToolTag> ToolTags { get; set; } = new List<ToolTag>();
    }
}
