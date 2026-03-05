using Microsoft.AspNetCore.Mvc;
using Tools.Application.DTOs.Tools;
using Tools.Application.Services;

namespace Tools.Api.Controllers.ToolController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly IToolService _service;

        public ToolsController(IToolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllToolsAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tool = await _service.GetToolByIdAsync(id);
            return tool is null ? NotFound() : Ok(tool);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var results = await _service.SearchToolsAsync(query);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<CreateToolResponse>> Create([FromBody] CreateToolRequest request)
        {
            var result = await _service.CreateToolAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToolRequest request)
        {
            return await _service.UpdateToolAsync(id, request) ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _service.DeleteToolAsync(id) ? NoContent() : NotFound();
        }
    }
}