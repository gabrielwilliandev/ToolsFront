
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tools.Application.DTOs.Tools;
using Tools.Application.UseCases.Comands;
using Tools.Application.UseCases.CreateTool;
using Tools.Application.UseCases.Queries;

namespace Tools.Api.Controllers.ToolController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly CreateToolUseCase _createToolUseCase;
        private readonly ToolQueryService _toolQueryService;
        private readonly UpdateToolUseCase _updateToolUseCase;
        private readonly DeleteToolUseCase _deleteToolUseCase;

        public ToolsController(CreateToolUseCase createToolUseCase, ToolQueryService toolQueryService, UpdateToolUseCase updateToolUseCase, DeleteToolUseCase deleteToolUseCase)
        {
            _createToolUseCase = createToolUseCase;
            _toolQueryService = toolQueryService;
            _updateToolUseCase = updateToolUseCase;
            _deleteToolUseCase = deleteToolUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tools = await _toolQueryService.GetAllAsync();
            return Ok(tools);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tool = await _toolQueryService.GetToolByIdAsync(id);
            if (tool == null)
                return NotFound();
            return Ok(tool);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            // Normaliza query para evitar NREs na camada de consulta
            var normalizedQuery = string.IsNullOrWhiteSpace(query) ? string.Empty : query.Trim();
            var tools = await _toolQueryService.SearchAsync(normalizedQuery);
            return Ok(tools);
        }

        [HttpPost]
        public async Task<ActionResult<CreateToolResponse>> Create([FromBody] CreateToolRequest request)
        {
            if (request is null)
                return BadRequest("Request body is required.");

            var createdTool = await _createToolUseCase.ExecuteAsync(
                request.Name,
                request.Description,
                request.Tags);

            // Tenta obter o recurso criado para preencher a resposta; se não existir, retorna pelo menos o Id.
            var tool = await _toolQueryService.GetToolByIdAsync(createdTool.Id);

            var response = tool is null
                ? new CreateToolResponse { Id = createdTool.Id }
                : new CreateToolResponse
                {
                    Id = tool.Id,
                    Name = tool.Name,
                    Description = tool.Description,
                    Tags = tool.Tags is null ? null : new List<string>(tool.Tags.Select(t => t.Name))
                };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToolRequest request)
        {
            if (request is null)
                return BadRequest("Request body is required.");
            var success = await _updateToolUseCase.ExecuteAsync(id, request.Name, request.Description, request.Tags);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _deleteToolUseCase.ExecuteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}