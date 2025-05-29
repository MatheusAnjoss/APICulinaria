using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceitasApi.Data;
using ReceitasApi.Model;

namespace ReceitasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceitasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReceitasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var receitas = await _context.Receitas
            .Include(r => r.Alergias)
            .AsNoTracking()
            .ToListAsync();
        return Ok(receitas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterReceitaPorIdAsync(int id)
    {
        try
        {
            var receita = await _context.Receitas
            .Include(r => r.Alergias)
            .FirstOrDefaultAsync(r => r.Id == id);

            if (receita == null)
                return NotFound();

            return Ok(receita);
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine($"Erro ao atualizar o banco de dados: {dbEx.Message}");
            return StatusCode(500, "Erro ao atualizar o banco de dados.");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> CriarReceitaAsync([FromBody] Receita novaReceita)
    {
        if (novaReceita == null || novaReceita.Alergias == null)
            return BadRequest("Dados Inválidos");

        if (novaReceita.Alergias.Any(alergia => alergia == null))
            return BadRequest("Uma ou mais alergias são inválidas.");

        try
        {
            await _context.Receitas.AddAsync(novaReceita);
            await _context.SaveChangesAsync();

            foreach (var alergia in novaReceita.Alergias)
            {
                alergia.ReceitaId = novaReceita.Id;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterReceitaPorIdAsync), new { id = novaReceita.Id }, novaReceita);
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine($"Erro ao atualizar o banco de dados: {dbEx.Message}");
            return StatusCode(500, "Erro ao atualizar o banco de dados.");
        }    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReceitaAsync(int id)
    {
        try
        {
            var receita = await _context.Receitas
                .Include(r => r.Alergias) 
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receita == null)
                return NotFound();
            
            _context.Alergias.RemoveRange(receita.Alergias);
            _context.Receitas.Remove(receita);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine($"Erro ao atualizar o banco de dados: {dbEx.Message}");
            return StatusCode(500, "Erro ao atualizar o banco de dados.");
        }
        catch (InvalidOperationException invalidOpEx)
        {
            Console.WriteLine($"Operação inválida: {invalidOpEx.Message}");
            return BadRequest("Operação inválida ao tentar deletar a receita.");
        }
    }
}