using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tools.Application.DTOs.Tools;
using Tools.Application.Interfaces;

namespace Tools.Api.Controllers.ToolController
{
    /// <summary>
    /// API para registro de ferramentas.
    /// as ferramentas podem ser cadastradas, editadas, listadas, buscadas e deletadas.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController(IToolService service) : ControllerBase
    {
        private readonly IToolService _service = service;

        /// <summary>
        /// Retorna uma lista de todas as ferramentas cadastradas.
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var tools = await _service.GetAllToolsAsync(userId);
            return Ok(tools);
        }

        /// <summary>
        /// Retorna os detalhes de uma ferramenta específica com base no seu ID.
        /// </summary>
        /// <param name="id">Identificador único da ferramenta</param>
        [Authorize]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ToolResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var tool = await _service.GetToolByIdAsync(id, userId);
            if (tool == null)
                return NotFound();

            return Ok(tool);
        }

        /// <summary>
        /// Busca ferramentas por termo (nome, descrição ou tag).
        /// </summary>
        /// <param name="query">Termo de pesquisa</param>
        [Authorize]
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ToolResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var userId = GetUserId();
            var tools = await _service.SearchToolsAsync(query, userId);
            return Ok(tools);
        }

        /// <summary>
        /// Cria uma nova ferramenta.
        /// </summary>
        /// <param name="request">Dados da ferramenta</param>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateToolResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateToolRequest request)
        {
            var userId = GetUserId();

            var tool = await _service.CreateToolAsync(request, userId);

            if (tool == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = tool.Id }, tool);
        }

        /// <summary>
        /// Atualiza uma ferramenta existente.
        /// </summary>
        /// <param name="id">ID da ferramenta</param>
        /// <param name="request">Novos dados</param>
        [Authorize]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToolRequest request)
        {
            var userId = GetUserId();
            var updated = await _service.UpdateToolAsync(id, request, userId);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Remove uma ferramenta pelo ID.
        /// </summary>
        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _service.DeleteToolAsync(id, userId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}