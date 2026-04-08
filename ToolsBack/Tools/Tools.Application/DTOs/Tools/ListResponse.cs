namespace Tools.Application.DTOs.Tools
{
    public class ListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ToolResponse> Tools { get; set; } = new List<ToolResponse>();
    }
}
