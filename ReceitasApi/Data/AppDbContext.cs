using Microsoft.EntityFrameworkCore;
using ReceitasApi.Model;

namespace ReceitasApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Receita> Receitas { get; set; }

    public DbSet<Alergias> Alergias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite("Data Source=receitas.db;Cache=Shared");

}