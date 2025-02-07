using FuncionarioManager.API.DTOs;
using FuncionarioManager.API.Models;
using FuncionarioManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FuncionarioManager.API.Controllers
{
    [Authorize(Roles = "Funcionario,Lider,Diretor")]
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        /// <summary>
        /// Cria um novo funcionário.
        /// </summary>
        /// <param name="funcionario">Os dados do funcionário a serem criados.</param>
        /// <returns>O funcionário criado.</returns>
        /// <response code="201">Retorna o funcionário criado.</response>
        /// <response code="400">Se houver um erro ao criar o funcionário.</response>
        [Authorize(Roles = "Lider,Diretor")]
        [HttpPost]
        public async Task<IActionResult> CreateFuncionario([FromBody] Funcionario funcionario)
        {
            try
            {
                var role = GetCurrentUser();

                var createdFuncionario = await _funcionarioService.CreateFuncionario(funcionario, role);
                return CreatedAtAction(nameof(GetFuncionarioById), new { id = createdFuncionario.Id }, createdFuncionario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém um funcionário pelo ID.
        /// </summary>
        /// <param name="id">O ID do funcionário.</param>
        /// <returns>O funcionário correspondente ao ID.</returns>
        /// <response code="200">Retorna o funcionário encontrado.</response>
        /// <response code="404">Se o funcionário não for encontrado.</response>
        [Authorize(Roles = "Lider,Diretor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuncionarioById(int id)
        {
            try
            {
                var funcionario = await _funcionarioService.GetFuncionarioById(id);
                return Ok(funcionario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todos os funcionários.
        /// </summary>
        /// <returns>Uma lista de todos os funcionários.</returns>
        /// <response code="200">Retorna a lista de funcionários.</response>
        [Authorize(Roles = "Lider,Diretor")]
        [HttpGet]
        public async Task<IActionResult> GetAllFuncionarios()
        {
            var funcionarios = await _funcionarioService.GetAllFuncionarios();
            return Ok(funcionarios);
        }

        /// <summary>
        /// Atualiza um funcionário existente.
        /// </summary>
        /// <param name="id">O ID do funcionário a ser atualizado.</param>
        /// <param name="funcionario">Os novos dados do funcionário.</param>
        /// <returns>Um status de sucesso.</returns>
        /// <response code="204">Se a atualização for bem-sucedida.</response>
        /// <response code="400">Se houver um erro ao atualizar o funcionário.</response>
        [Authorize(Roles = "Lider,Diretor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuncionario(int id, [FromBody] FuncionarioDTO funcionario)
        {
            try
            {
                var role = GetCurrentUser();

                // Lógica para atualizar o funcionário
                await _funcionarioService.UpdateFuncionario(id, funcionario, role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um funcionário pelo ID.
        /// </summary>
        /// <param name="id">O ID do funcionário a ser removido.</param>
        /// <returns>Um status de sucesso.</returns>
        /// <response code="204">Se a remoção for bem-sucedida.</response>
        /// <response code="404">Se o funcionário não for encontrado.</response>
        [Authorize(Roles = "Lider,Diretor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            try
            {
                await _funcionarioService.DeleteFuncionario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todos os líderes e diretores.
        /// </summary>
        /// <returns>Uma lista de líderes e diretores.</returns>
        /// <response code="200">Retorna a lista de líderes e diretores.</response>
        [HttpGet("lider-e-diretor")]
        public async Task<IActionResult> GetLiderAndDiretor()
        {
            var funcionarios = await _funcionarioService.GetFuncionariosByRoles(new[] { Role.Lider, Role.Diretor });
            return Ok(funcionarios);
        }

        private Role GetCurrentUser()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim == null)
            {
                throw new UnauthorizedAccessException("Usuário não possui um papel definido.");
            }

            // Converte a string do papel para o enum Role
            return (Role)Enum.Parse(typeof(Role), roleClaim);
        }
    }
}