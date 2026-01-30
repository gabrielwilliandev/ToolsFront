namespace WebApplication1.Domain.Entities
{
    public class ToolTag
    {
        public int ToolId { get; set; }
        public ToolsEntitie Tool { get; set; } = null!;
        public int TagId { get; set; }
        public TagEntitie Tag { get; set; } = null!;
    }
}
