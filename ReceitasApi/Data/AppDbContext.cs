using Microsoft.EntityFrameworkCore;
using ReceitasApi.Model;

namespace ReceitasApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Receita> Receitas { get; set; }

    public DbSet<Alergias> Alergias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite("Data Source=receitas.db;Cache=Shared");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Receita>().HasData(
            new Receita {
                Id = 1,
                Nome = "Bolo de Cenoura",
                Descricao = "Bolo simples de cenoura",
                Ingredientes = new List<string> { "cenoura", "farinha", "ovos", "açúcar" },
                ModoPreparo = "Misture tudo e asse.",
                TempoPreparoMin = 40,
                Valor = 25.00m
            },
            new Receita {
                Id = 2,
                Nome = "Pão de Queijo",
                Descricao = "Pão de queijo mineiro",
                Ingredientes = new List<string> { "polvilho", "queijo", "ovos", "leite" },
                ModoPreparo = "Misture, modele e asse.",
                TempoPreparoMin = 30,
                Valor = 18.50m
            },
            new Receita {
                Id = 3,
                Nome = "Moqueca de Peixe",
                Descricao = "Moqueca baiana tradicional",
                Ingredientes = new List<string> { "peixe", "pimentão", "cebola", "leite de coco" },
                ModoPreparo = "Cozinhe tudo junto.",
                TempoPreparoMin = 50,
                Valor = 45.00m
            }
        );
    }
}