using FluentAssertions;
using Moq;
using SocialApp.Interfaces;
using SocialApp.model;
using SocialApp.Services;
using SocialApp.ViewModels;

namespace SocialApp.Teste
{
    public class UsuarioServiceTest
    {
        [Fact]
        public async Task CreateUsuarioAsync_DeveRetornarUsuarioOutputViewModel()
        {
            // Arrange
            var repoMock = new Mock<IUsuarioRepository>();

            // Configura o Mock para retornar o objeto que receber
            repoMock.Setup(r => r.CreateUsuarioAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync((Usuario u) => u);

            var service = new UsuarioService(repoMock.Object);

            var usuarioInputViewModel = new UsuarioInputViewModel
            {
                Nome = "Clayton",
                Email = "clayton@example.com"
            };

            // Act
            var usuarioOutput = await service.CreateUsuarioAsync(usuarioInputViewModel);

            // Assert
            usuarioOutput.Should().NotBeNull();
            usuarioOutput.Nome.Should().Be("Clayton");
            usuarioOutput.Email.Should().Be("clayton@example.com");

            // Verifica se o repositório foi chamado
            repoMock.Verify(r => r.CreateUsuarioAsync(It.Is<Usuario>(u => u.Nome == "Clayton")), Times.Once);
        }
    }
}