// file: Tools.Api\Controllers\ToolController\ToolsController.cs
using Microsoft.AspNetCore.Mvc;
using Tools.Application.DTOs.Tools;
using Tools.Application.Services;

namespace Tools.Api.Controllers.ToolController
{
    /// <summary>
    /// API para registro de ferramentas.
    /// as ferramentas podem ser cadastradas, editadas, listadas, buscadas e deletadas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly IToolService _service;

        public ToolsController(IToolService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de todas as ferramentas cadastradas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tools = await _service.GetAllToolsAsync();
            return Ok(tools);
        }

        /// <summary>
        /// Retorna os detalhes de uma ferramenta específica com base no seu ID.
        /// </summary>
        /// <param name="id">Identificador único da ferramenta</param>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ToolResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tool = await _service.GetToolByIdAsync(id);
            if (tool == null)
                return NotFound();

            return Ok(tool);
        }

        /// <summary>
        /// Busca ferramentas por termo (nome, descrição ou tag).
        /// </summary>
        /// <param name="query">Termo de pesquisa</param>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ToolResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var tools = await _service.SearchToolsAsync(query);
            return Ok(tools);
        }

        /// <summary>
        /// Cria uma nova ferramenta.
        /// </summary>
        /// <param name="request">Dados da ferramenta</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateToolResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateToolRequest request)
        {
            var tool = await _service.CreateToolAsync(request);
            if (tool == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = tool.Id }, tool);
        }

        /// <summary>
        /// Atualiza uma ferramenta existente.
        /// </summary>
        /// <param name="id">ID da ferramenta</param>
        /// <param name="request">Novos dados</param>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToolRequest request)
        {
            var updated = await _service.UpdateToolAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Remove uma ferramenta pelo ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteToolAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Endpoint para teste de exceção inesperada.
        /// </summary>
        [HttpGet("erro")]
        public IActionResult GetError()
        {
            throw new Exception("Erro de teste");
        }
    }
}