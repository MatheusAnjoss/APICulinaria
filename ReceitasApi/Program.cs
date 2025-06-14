using Microsoft.EntityFrameworkCore;
using ReceitasApi.Data;
using ReceitasApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints Minimal API
app.MapGet("/api/receitas", async (AppDbContext db) =>
    await db.Receitas.Include(r => r.Alergias).ToListAsync());

app.MapGet("/api/receitas/{id}", async (int id, AppDbContext db) =>
    await db.Receitas.Include(r => r.Alergias).FirstOrDefaultAsync(r => r.Id == id)
        is Receita receita ? Results.Ok(receita) : Results.NotFound());

app.MapPost("/api/receitas", async (Receita receita, AppDbContext db) =>
{
    db.Receitas.Add(receita);
    await db.SaveChangesAsync();
    return Results.Created($"/api/receitas/{receita.Id}", receita);
});

app.MapPut("/api/receitas/{id}", async (int id, Receita input, AppDbContext db) =>
{
    var receita = await db.Receitas.Include(r => r.Alergias).FirstOrDefaultAsync(r => r.Id == id);
    if (receita is null) return Results.NotFound();
    receita.Nome = input.Nome;
    receita.Descricao = input.Descricao;
    receita.Ingredientes = input.Ingredientes;
    receita.ModoPreparo = input.ModoPreparo;
    receita.TempoPreparoMin = input.TempoPreparoMin;
    receita.Alergias = input.Alergias;
    await db.SaveChangesAsync();
    return Results.Ok(receita);
});

app.MapDelete("/api/receitas/{id}", async (int id, AppDbContext db) =>
{
    var receita = await db.Receitas.FindAsync(id);
    if (receita is null) return Results.NotFound();
    db.Receitas.Remove(receita);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Relatório: Ingredientes mais usados
app.MapGet("/api/relatorio/ingredientes-mais-usados", async (AppDbContext db) =>
{
    var ingredientes = await db.Receitas
        .SelectMany(r => r.Ingredientes)
        .GroupBy(i => i)
        .Select(g => new { Ingrediente = g.Key, Quantidade = g.Count() })
        .OrderByDescending(g => g.Quantidade)
        .ToListAsync();
    return ingredientes;
});

app.Run();