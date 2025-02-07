using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.Strategies
{
    public class FuncionarioStrategy : IRoleStrategy
    {
        public bool CanAdd(Role role)
        {
            return role == Role.Funcionario;
        }
    }
}
