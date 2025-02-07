using FuncionarioManager.API.DTOs;
using FuncionarioManager.API.Repositories;
using FuncionarioManager.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuncionarioManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _authService.AuthenticateAsync(loginDto.Email, loginDto.Senha);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }
    }
}
