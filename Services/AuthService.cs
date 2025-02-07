using FuncionarioManager.API.Models;
using FuncionarioManager.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FuncionarioManager.API.Services
{
    public class AuthService
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IFuncionarioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(string email, string senha)
        {
            var funcionario = await _repository.GetByEmailAndPasswordAsync(email, senha);
            if (funcionario == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, funcionario.Id.ToString()),
                    new Claim(ClaimTypes.Name, funcionario.Email),
                    new Claim(ClaimTypes.Role, funcionario.Role.ToString()) // Adicionando a role
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
