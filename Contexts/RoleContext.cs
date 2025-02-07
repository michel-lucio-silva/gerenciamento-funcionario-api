using FuncionarioManager.API.Models;
using FuncionarioManager.API.Strategies;

namespace FuncionarioManager.API.Contexts
{
    public class RoleContext
    {
        private IRoleStrategy _roleStrategy;

        public RoleContext(Role role)
        {
            SetStrategy(role);
        }

        public void SetStrategy(Role role)
        {
            switch (role)
            {
                case Role.Funcionario:
                    _roleStrategy = new FuncionarioStrategy();
                    break;
                case Role.Lider:
                    _roleStrategy = new LiderStrategy();
                    break;
                case Role.Diretor:
                    _roleStrategy = new DiretorStrategy();
                    break;
                default:
                    throw new ArgumentException("Role não reconhecida");
            }
        }

        public bool CanAdd(Role role)
        {
            return _roleStrategy.CanAdd(role);
        }

        public bool CanUpdate(Role role)
        {
            return _roleStrategy.CanAdd(role); 
        }
    }
}
