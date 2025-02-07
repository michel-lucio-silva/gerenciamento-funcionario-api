﻿using System.Threading.Tasks;
using FuncionarioManager.API.Models;
using FuncionarioManager.API.Repositories;
using FuncionarioManager.API.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace FuncionarioManager.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IFuncionarioRepository> _mockRepository;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockRepository = new Mock<IFuncionarioRepository>();
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c["Jwt:Key"]).Returns("EstaÉUmaChaveSecretaMuitoLongaParaOJWT1234567890");
            configuration.Setup(c => c["Jwt:Issuer"]).Returns("localhost");
            configuration.Setup(c => c["Jwt:Audience"]).Returns("localhost");

            _authService = new AuthService(_mockRepository.Object, configuration.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var funcionario = new Funcionario { Id = 1, Email = "admin@example.com", Senha = "senhaSegura" };
            _mockRepository.Setup(repo => repo.GetByEmailAndPasswordAsync("admin@example.com", "senhaSegura"))
                           .ReturnsAsync(funcionario);

            // Act
            var token = await _authService.AuthenticateAsync("admin@example.com", "senhaSegura");

            // Assert
            Assert.NotNull(token);
            // Adicione mais verificações conforme necessário
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByEmailAndPasswordAsync("admin@example.com", "senhaErrada"))
                           .ReturnsAsync((Funcionario)null);

            // Act
            var token = await _authService.AuthenticateAsync("admin@example.com", "senhaErrada");

            // Assert
            Assert.Null(token);
        }
    }
}
