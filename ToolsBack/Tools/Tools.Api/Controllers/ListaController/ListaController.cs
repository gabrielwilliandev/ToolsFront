using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tools.Application.DTOs.Tools;
using Tools.Application.Interfaces;

namespace Tools.Api.Controllers.ListaController
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de listas do usuário.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ListaController : ControllerBase
    {
        private readonly IListaService _service;

        /// <summary>
        /// Construtor da controller de listas.
        /// </summary>
        /// <param name="service">Serviço de listas</param>
        public ListaController(IListaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Cria uma nova lista para o usuário autenticado.
        /// </summary>
        /// <param name="request">Dados da lista a ser criada</param>
        /// <returns>Lista criada</returns>
        /// <response code="200">Lista criada com sucesso</response>
        /// <response code="400">Erro de validação</response>
        [HttpPost]
        public async Task<ActionResult<ListResponse>> Create([FromBody] CreateListRequest request)
        {
            var userId = GetUserId();

            var result = await _service.CreateListaAsync(request, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        /// Retorna todas as listas do usuário autenticado.
        /// </summary>
        /// <returns>Lista de listas</returns>
        /// <response code="200">Listas retornadas com sucesso</response>
        [HttpGet]
        public async Task<ActionResult<List<ListResponse>>> GetAll()
        {
            var userId = GetUserId();

            var result = await _service.GetAllListasAsync(userId);

            return Ok(result);
        }

        /// <summary>
        /// Retorna uma lista específica pelo ID.
        /// </summary>
        /// <param name="id">ID da lista</param>
        /// <returns>Lista encontrada</returns>
        /// <response code="200">Lista encontrada</response>
        /// <response code="404">Lista não encontrada</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ListResponse>> GetById(Guid id)
        {
            var userId = GetUserId();

            var result = await _service.GetListaByIdAsync(id, userId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Atualiza uma lista existente.
        /// </summary>
        /// <param name="id">ID da lista</param>
        /// <param name="request">Novos dados da lista</param>
        /// <returns>Status da operação</returns>
        /// <response code="204">Atualizado com sucesso</response>
        /// <response code="404">Lista não encontrada</response>
        /// <response code="400">Erro de validação</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateListRequest request)
        {
            var userId = GetUserId();

            var success = await _service.UpdateListaAsync(id, request, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Remove uma lista pelo ID.
        /// </summary>
        /// <param name="id">ID da lista</param>
        /// <returns>Status da operação</returns>
        /// <response code="204">Removido com sucesso</response>
        /// <response code="404">Lista não encontrada</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userId = GetUserId();

            var success = await _service.DeleteListaAsync(id, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Obtém o ID do usuário autenticado a partir do token JWT.
        /// </summary>
        /// <returns>Guid do usuário</returns>
        /// <exception cref="UnauthorizedAccessException">Caso o usuário não esteja autenticado corretamente</exception>
        private Guid GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null || !Guid.TryParse(claim.Value, out var userId))
                throw new UnauthorizedAccessException();

            return userId;
        }
    }
}