using FuncionarioManager.API.Controllers;
using FuncionarioManager.API.Models;
using FuncionarioManager.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FuncionarioManager.Tests
{
    public class FuncionarioControllerTests
    {
        private readonly Mock<IFuncionarioService> _mockService;
        private readonly FuncionarioController _controller;

        public FuncionarioControllerTests()
        {
            _mockService = new Mock<IFuncionarioService>();
            _controller = new FuncionarioController(_mockService.Object);
        }

        [Fact]
        public async Task CriarFuncionario_Valido_CriaFuncionario()
        {
            var funcionario = new Funcionario
            {
                Nome = "Admin",
                Sobrenome = "User ",
                Email = "admin@example.com",
                Senha = "admin123",
                DataNascimento = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc),
                Telefone = new List<string> { "119746574" },
                NumeroDocumento = "1134567890",
                Role = Role.Diretor
            };

            _mockService.Setup(service => service.CreateFuncionario(funcionario,Role.Diretor))
                         .ReturnsAsync(funcionario);

            var result = await _controller.CreateFuncionario(funcionario);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedFuncionario = Assert.IsType<Funcionario>(createdResult.Value);
            Assert.Equal(funcionario.Nome, returnedFuncionario.Nome);
            Assert.Equal(funcionario.Sobrenome, returnedFuncionario.Sobrenome);
            _mockService.Verify(service => service.CreateFuncionario(funcionario, Role.Diretor), Times.Once);
        }

        [Fact]
        public async Task CriarFuncionario_IdadeInvalida_RetornaErro()
        {
            var funcionario = new Funcionario
            {
                Nome = "Admin",
                Sobrenome = "User ",
                Email = "admin@example.com",
                Senha = "admin123",
                DataNascimento = DateTime.SpecifyKind(new DateTime(2015, 1, 1), DateTimeKind.Utc),
                Telefone = new List<string> { "119746574" },
                NumeroDocumento = "1334567890",
                Role = Role.Diretor
            };

            _mockService.Setup(service => service.CreateFuncionario(funcionario, Role.Diretor))
                         .ThrowsAsync(new Exception("Funcionário deve ter pelo menos 18 anos."));

            var result = await _controller.CreateFuncionario(funcionario);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Funcionário deve ter pelo menos 18 anos.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetLiderAndDiretor_RetornaSomenteLiderEDiretor()
        {
            var funcionarios = new List<Funcionario>
            {
                new Funcionario { Nome = "Lider1", Role = Role.Lider },
                new Funcionario { Nome = "Diretor1", Role = Role.Diretor },
                new Funcionario { Nome = "Funcionario1", Role = Role.Funcionario }
            };

            _mockService.Setup(service => service.GetFuncionariosByRoles(It.IsAny<Role[]>()))
                         .ReturnsAsync(funcionarios.Where(f => f.Role == Role.Lider || f.Role == Role.Diretor).ToList());

            var result = await _controller.GetLiderAndDiretor();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFuncionarios = Assert.IsAssignableFrom<List<Funcionario>>(okResult.Value);
            Assert.Equal(2, returnedFuncionarios.Count);
            Assert.All(returnedFuncionarios, f => Assert.True(f.Role == Role.Lider || f.Role == Role.Diretor));
        }
    }
}
