using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.Strategies
{
    public interface IRoleStrategy
    {
        bool CanAdd(Role role);
    }
}
