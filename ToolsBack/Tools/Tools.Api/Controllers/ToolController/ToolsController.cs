using Microsoft.AspNetCore.Mvc;
using Tools.Application.DTOs.Tools;
using Tools.Application.Services;

namespace Tools.Api.Controllers.ToolController
{
    ///<summary>
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
        /// <returns>Lista de ferramentas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllToolsAsync();    
            return Ok(result.Value);
        }

        /// <summary>
        /// Retorna os detalhes de uma ferramenta específica com base no seu ID.
        /// </summary>
        /// <param name="id">Identificador único da ferramenta</param>
        /// <returns>Detalhes da ferramenta ou NotFound se não existir</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ToolResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetToolByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Permite buscar ferramentas com base em um termo de pesquisa, 
        /// que pode corresponder ao nome ou à descrição da ferramenta.
        /// </summary>
        /// <param name="query">Texto usado na busca</param>
        /// <returns>Lista de ferramentas que correspondem ao termo de pesquisa</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ToolResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var results = await _service.SearchToolsAsync(query);
            if (!results.IsSuccess)
            {
                return BadRequest(results.Errors);
            }
            return Ok(results.Value);
        }


        /// <summary>
        /// Cria uma nova ferramenta com base nos dados fornecidos no corpo da requisição.
        /// </summary>
        /// <param name="request">Dados da ferramenta a ser criada</param>
        /// <returns>Detalhes da ferramenta criada ou BadRequest se os dados forem inválidos</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateToolResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateToolResponse>> Create([FromBody] CreateToolRequest request)
        {
            var result = await _service.CreateToolAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza os detalhes de uma ferramenta existente com base no 
        /// seu ID e nos dados fornecidos no corpo da requisição.
        /// </summary>
        /// <param name="id">Identificador da ferramenta</param>
        /// <param name="request">Novos dados da ferramenta</param>
        /// <returns>NoContent se a atualização for bem-sucedida ou NotFound se a ferramenta não existir</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToolRequest request)
        {
            var result = await _service.UpdateToolAsync(id, request);
            if (!result.IsSuccess)
            {
                if (result.Errors.Any(e => e.Code == "NotFound"))
                {
                    return NotFound(result.Errors);
                }
                return BadRequest(result.Errors);
            }
            return NoContent();
        }

        /// <summary>
        /// Deleta uma ferramenta existente com base no seu ID.
        /// </summary>
        /// <param name="id">Identificador da ferramenta</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound se a ferramenta não existir</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteToolAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }
            return NoContent();
        }

        [HttpGet("erro")]
        public IActionResult GetError()
        {
            throw new Exception("Erro de teste");
        }
    }
}