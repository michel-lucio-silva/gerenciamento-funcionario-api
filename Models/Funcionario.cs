namespace FuncionarioManager.API.Models
{
    using System;
    using System.Collections.Generic;

    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string NumeroDocumento { get; set; }
        public List<string> Telefone { get; set; }
        public int? NomeGestor { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

        public Role Role { get; set; }
    }
}
