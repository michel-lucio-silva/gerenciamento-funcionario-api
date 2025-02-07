using FuncionarioManager.API.DTOs;
using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.Services
{
    public interface IFuncionarioService
    {
        Task<Funcionario> CreateFuncionario(Funcionario funcionario, Role currentUser);
        Task<Funcionario> GetFuncionarioById(int id);
        Task<IEnumerable<Funcionario>> GetAllFuncionarios();
        Task UpdateFuncionario(int id, FuncionarioDTO funcionario, Role currentUser);
        Task DeleteFuncionario(int id);

        Task<List<Funcionario>> GetFuncionariosByRoles(Role[] roles);
    }
}
