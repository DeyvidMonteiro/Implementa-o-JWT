using Jwt_Implementacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Implementacao.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UsuarioModel> Usuario { get; set; }

}
