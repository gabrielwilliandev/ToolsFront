using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.API.Enums;
using WebApplication1.Application.DTOs;
using WebApplication1.Application.Services;

namespace WebApplication1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly IToolsService _service;
        public ToolsController(IToolsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? tag)
        {
            var tools = await _service.GetBy(tag);
            return StatusCode((int)Codes.Ok, tools);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTool([FromBody] CreateToolDto toolDto)
        {
            var createdTool = await _service.CreateTool(toolDto);
            return StatusCode((int)Codes.Created, createdTool);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTool(int id)
        {
            await _service.DeleteTool(id);
            return NoContent();
        }
    }
}
