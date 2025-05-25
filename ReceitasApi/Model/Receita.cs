using ReceitasApi.Data;

namespace ReceitasApi.Model;

public class Receita
{
    public int Id { get; private set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public List<string> Ingredientes { get; set; }
    public string ModoPreparo { get; set; }
    public int TempoPreparoMin { get; set; }
    public List<Alergias> Alergias { get; set; }
}

