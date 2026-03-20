using Microsoft.AspNetCore.Mvc;
using Tools.Application.DTOs.Contacts;
using Tools.Application.Interfaces;

namespace Tools.Api.Controllers.EmailController
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IContactService _contactService;
        public EmailController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        public async Task<IActionResult> Enviar([FromBody] ContactRequest request)
        {
            await _contactService.SendContactAsync(request);

            return Ok();
        }
    }
}
