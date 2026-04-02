using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tools.Application.DTOs.Auth;
using Tools.Application.Interfaces;
using Tools.Application.Services.Auth;
using Tools.Domain.Entities;
using Tools.Infrastructure.Repositories;

namespace Tools.Api.Controllers.AuthController;

    [ApiController]
    [Route("api/[controller]")]
public class AuthController : ControllerBase
    {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    /// <summary>
    /// Endpoint de login para autenticar um usuário e obter um token JWT.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        return Ok(new { Token = token });
    }

    /// <summary>
    /// Endpoint de registro para criar um novo usuário e obter um token JWT.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        await _authService.RegisterAsync(dto);
        return Ok();
    }
    
}

