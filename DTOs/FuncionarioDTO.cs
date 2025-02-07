using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.DTOs
{
    public class FuncionarioDTO
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string NumeroDocumento { get; set; }
        public List<string> Telefone { get; set; }
        public int? NomeGestor { get; set; }
        public DateTime DataNascimento { get; set; }

        public Role Role { get; set; }
    }
}
