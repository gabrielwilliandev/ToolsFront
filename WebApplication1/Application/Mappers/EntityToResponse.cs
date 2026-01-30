using WebApplication1.Application.DTOs;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.Mappers
{
    public static class EntityToResponse
    {
        public static ToolsResponseDto MapToResponse(this ToolsEntitie tool)
        {
            return new ToolsResponseDto
            {
                Id = tool.ToolId,
                Title = tool.Title,
                Link = tool.Link,
                Description = tool.Description,
                Tags = tool.ToolTags.Select(tt => tt.Tag.Name)
            };
        }
    }
}
