using FuncionarioManager.API.Data;
using FuncionarioManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuncionarioManager.API.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Funcionario> GetByIdAsync(int id)
        {
            return await _context.Funcionarios.FindAsync(id);
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        public async Task AddAsync(Funcionario funcionario)
        {
            await _context.Funcionarios.AddAsync(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var funcionario = await GetByIdAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Funcionario> GetByEmailAndPasswordAsync(string email, string senha)
        {
            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Email == email);
            if (funcionario != null && BCrypt.Net.BCrypt.Verify(senha, funcionario.Senha))
            {
                return funcionario;
            }

            return null;
        }

        public async Task<Funcionario> GetByNumeroDocumentoAsync(string numeroDocumento)
        {
            return await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.NumeroDocumento == numeroDocumento);
        }

        public async Task<List<Funcionario>> GetByRolesAsync(Role[] roles)
        {
            return await _context.Funcionarios
                                 .Where(f => roles.Contains(f.Role))
                                 .ToListAsync();
        }

    }
}
