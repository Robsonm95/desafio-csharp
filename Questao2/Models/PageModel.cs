using System.Text.Json.Serialization;

namespace Questao2.Models;

public class PageModel
{
    [JsonPropertyName("page")]
    public int Pagina { get; set; }

    [JsonPropertyName("per_page")]
    public int QtdPorPagina { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPaginas { get; set; }

    [JsonPropertyName("data")]
    public JogosFutebolModel[]? Data { get; set; }
}
