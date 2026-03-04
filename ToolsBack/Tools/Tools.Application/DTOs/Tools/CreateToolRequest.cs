namespace Tools.Application.DTOs.Tools
{
    public class CreateToolRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
