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


    [HttpPost]
    public IActionResult CriarReceita([FromBody] Receita novaReceita)
    {
        if (novaReceita == null)
            return BadRequest("Dados Inválidos");

        foreach (var alergia in novaReceita.Alergias)
        {
            alergia.ReceitaId = novaReceita.Id;
        }

        _context.Receitas.Add(novaReceita);
        _context.SaveChanges();

        return CreatedAtAction(nameof(ObterReceitaPorId), new { id = novaReceita.Id }, novaReceita);
    }


    [HttpGet("{id}")]
    public IActionResult ObterReceitaPorId(int id)

    {
        var receita = _context.Receitas
        .Include(r => r.Alergias)
        .FirstOrDefault(r => r.Id == id);

        if (receita == null)
            return NotFound();

            return Ok(receita);
        }
}