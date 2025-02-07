using FuncionarioManager.API.Models;

namespace FuncionarioManager.API.Repositories
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario> GetByIdAsync(int id);
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task AddAsync(Funcionario funcionario);
        Task UpdateAsync(Funcionario funcionario);
        Task DeleteAsync(int id);
        Task<Funcionario> GetByEmailAndPasswordAsync(string email, string senha);
        Task<Funcionario> GetByNumeroDocumentoAsync(string numeroDocumento);
        Task<List<Funcionario>> GetByRolesAsync(Role[] roles);
    }
}
