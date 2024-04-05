using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Questao2.Models;

public class JogosFutebolModel
{
    [JsonPropertyName("competition")]
    public string Competition { get; set; } = string.Empty;

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("round")]
    public string Round { get; set; } = string.Empty;

    [JsonPropertyName("team1")]
    public string Team1 { get; set; } = string.Empty;

    [JsonPropertyName("team2")]
    public string Team2 { get; set; } = string.Empty;

    [JsonPropertyName("team1goals")]
    public string Team1Goals { get; set; } = string.Empty;

    [JsonPropertyName("team2goals")]
    public string Team2Goals { get; set; } = string.Empty;
}
