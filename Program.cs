using FuncionarioManager.API.Data;
using FuncionarioManager.API.Models;
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            await SeedDatabase(context);
        }
        await host.RunAsync();
    }

    private static async Task SeedDatabase(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

       if (!context.Funcionarios.Any())
        {
            context.Funcionarios.Add(new Funcionario
            {
                Nome = "Admin",
                Sobrenome = "User ",
                Email = "admin@admin.com",
                Senha = BCrypt.Net.BCrypt.HashPassword("admin123"),
                DataNascimento = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc), 
                Telefone = new List<string> { "119746574" },
                NumeroDocumento = "1234567890",
                Role = Role.Diretor
            });
            await context.SaveChangesAsync();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}