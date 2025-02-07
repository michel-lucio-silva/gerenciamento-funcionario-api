using FuncionarioManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FuncionarioManager.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
