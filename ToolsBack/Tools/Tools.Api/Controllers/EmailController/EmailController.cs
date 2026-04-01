using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.Application.DTOs.Contacts;
using Tools.Application.Interfaces;

namespace Tools.Api.Controllers.EmailController
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController(IContactService service) : ControllerBase
    {
        private readonly IContactService _contactService = service;

        /// <summary>
        /// Endpoint para enviar um contato. O usuário deve estar autenticado para acessar este endpoint.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Enviar([FromBody] ContactRequest request)
        {
            await _contactService.SendContactAsync(request);

            return Ok();
        }
    }
}
