using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceitasApi.Data;

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
}