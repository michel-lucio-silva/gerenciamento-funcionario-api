using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.Strategies
{
    public class DiretorStrategy : IRoleStrategy
    {
        public bool CanAdd(Role role)
        {
            return true;
        }
    }
}
