using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.Strategies
{
    public class LiderStrategy : IRoleStrategy
    {
        public bool CanAdd(Role role)
        {
            return role == Role.Funcionario || role == Role.Diretor;
        }
    }
}
