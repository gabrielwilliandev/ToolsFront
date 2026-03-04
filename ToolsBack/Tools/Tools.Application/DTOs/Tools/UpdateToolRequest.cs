namespace Tools.Application.DTOs.Tools
{
    public class UpdateToolRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
