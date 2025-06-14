using ReceitasApi.Data;

namespace ReceitasApi.Model;

public class Receita
{
    public int Id { get; set; } // set público para seed
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public List<string>? Ingredientes { get; set; }
    public string? ModoPreparo { get; set; }
    public int TempoPreparoMin { get; set; }
    public decimal Valor { get; set; } // Valor/preço da receita
    public List<Alergias>? Alergias { get; set; }
}

