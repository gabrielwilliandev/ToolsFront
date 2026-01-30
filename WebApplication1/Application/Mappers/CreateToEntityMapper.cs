using WebApplication1.Application.DTOs;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.Mappers
{
    public static class CreateToEntityMapper
    {
        public static ToolsEntitie MapToEntity(this CreateToolDto toolCreateDto)
        {
            return new ToolsEntitie
            {
                Title = toolCreateDto.Title,
                Link = toolCreateDto.Link,
                Description = toolCreateDto.Description,
            };
        }
    }
}
