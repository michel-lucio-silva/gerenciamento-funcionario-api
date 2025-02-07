using FuncionarioManager.API.DTOs;
using FuncionarioManager.API.Models;
using FuncionarioManager.API.Repositories;
using FuncionarioManager.API.Contexts;

namespace FuncionarioManager.API.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _repository;
        

        public FuncionarioService(IFuncionarioRepository repository)
        {
            _repository = repository;
        }


        public async Task<Funcionario> GetFuncionarioById(int id)
        {
            var funcionario = await _repository.GetByIdAsync(id);
            if (funcionario == null)
            {
                throw new Exception("Funcionário não encontrado.");
            }
            return funcionario;
        }

        public async Task<IEnumerable<Funcionario>> GetAllFuncionarios()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Funcionario> CreateFuncionario(Funcionario funcionario, Role currentUser)
        {
            var roleContext = new RoleContext(currentUser);
           
            if (!roleContext.CanAdd(funcionario.Role))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para adicionar este tipo de funcionário.");
            }

            if (DateTime.UtcNow.Year - funcionario.DataNascimento.Year < 18)
            {
                throw new Exception("Funcionário deve ter pelo menos 18 anos.");
            }

            var existeFuncionario = await _repository.GetByNumeroDocumentoAsync(funcionario.NumeroDocumento);
            if (existeFuncionario != null)
            {
                throw new Exception("Número do documento já está em uso.");
            }

             if (string.IsNullOrEmpty(funcionario.Senha))
            {
                throw new ArgumentNullException(nameof(funcionario.Senha), "A senha não pode ser nula.");
            }

            funcionario.DataNascimento = DateTime.SpecifyKind(funcionario.DataNascimento, DateTimeKind.Utc);
            funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);

            await _repository.AddAsync(funcionario);
            return funcionario;
        }

        public async Task UpdateFuncionario(int id, FuncionarioDTO funcionario, Role currentUser)
        {
            var roleContext = new RoleContext(currentUser);

            if (!roleContext.CanUpdate(funcionario.Role))
            {
                throw new UnauthorizedAccessException("Você não tem permissão para atualizar este tipo de funcionário.");
            }

            var existeFuncionario = await _repository.GetByNumeroDocumentoAsync(funcionario.NumeroDocumento);
            if (existeFuncionario != null)
            {
                throw new Exception("Número do documento já está em uso.");
            }

            if (funcionario == null)
            {
                throw new Exception("Dados não recebidos.");
            }

            if (DateTime.UtcNow.Year - funcionario.DataNascimento.Year < 18)
            {
                throw new Exception("Funcionário deve ter pelo menos 18 anos.");
            }

            var existingFuncionario = await _repository.GetByIdAsync(id);
            if (existingFuncionario == null)
            {
                throw new Exception("Funcionário não encontrado.");
            }

            existingFuncionario.Nome = funcionario.Nome;
            existingFuncionario.Sobrenome = funcionario.Sobrenome;
            existingFuncionario.Email = funcionario.Email;
            existingFuncionario.NumeroDocumento = funcionario.NumeroDocumento;
            existingFuncionario.Telefone = funcionario.Telefone;
            existingFuncionario.NomeGestor = funcionario.NomeGestor;
            existingFuncionario.DataNascimento = DateTime.SpecifyKind(funcionario.DataNascimento, DateTimeKind.Utc);
            existingFuncionario.Role = funcionario.Role;

            await _repository.UpdateAsync(existingFuncionario);
        }

        public async Task DeleteFuncionario(int id)
        {
            var existingFuncionario = await _repository.GetByIdAsync(id);
            if (existingFuncionario == null)
            {
                throw new Exception("Funcionário não encontrado.");
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<List<Funcionario>> GetFuncionariosByRoles(Role[] roles)
        {
            return await _repository.GetByRolesAsync(roles);
        }
    }
}
