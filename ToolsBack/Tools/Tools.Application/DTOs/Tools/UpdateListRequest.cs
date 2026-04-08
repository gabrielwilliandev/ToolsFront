namespace Tools.Application.DTOs.Tools
{
    public class UpdateListRequest
    {
        public string Name { get; set; }
        public List<UpdateToolRequest> Tools { get; set; } = new();
    }
}
