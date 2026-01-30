namespace WebApplication1.Application.DTOs
{
    public class ToolsResponseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
