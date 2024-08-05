using Newtonsoft.Json;

namespace Bext.Reps.Business.Commons.Models;
public class ControlDocResponse
{
    [JsonProperty("RESPUESTA", NullValueHandling = NullValueHandling.Ignore)]
    public bool Respuesta { get; set; }

    [JsonProperty("REDIRECT", NullValueHandling = NullValueHandling.Ignore)]
    public string? Redirect { get; set; }

    [JsonProperty("MENSAJE", NullValueHandling = NullValueHandling.Ignore)]
    public string Mensaje { get; set; } = string.Empty;

    [JsonProperty("VALORESRESPUESTA", NullValueHandling = NullValueHandling.Ignore)]
    public string ValoresRespuesta { get; set; } = string.Empty;

    [JsonProperty("IDRESPUESTA", NullValueHandling = NullValueHandling.Ignore)]
    public int IdRespuesta { get; set; }

    [JsonProperty("VALORDECIMAL", NullValueHandling = NullValueHandling.Ignore)]
    public decimal ValorDecimal { get; set; }

    [JsonProperty("OBJETOS", NullValueHandling = NullValueHandling.Ignore)]
    public string? Objetos { get; set; }

    [JsonProperty("OBJETOS2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Objetos2 { get; set; }

    [JsonProperty("OBJETOS3", NullValueHandling = NullValueHandling.Ignore)]
    public string? Objetos3 { get; set; }

    [JsonProperty("OBJETOS4", NullValueHandling = NullValueHandling.Ignore)]
    public string? Objetos4 { get; set; }

    [JsonProperty("RUTABASE", NullValueHandling = NullValueHandling.Ignore)]
    public string RutaBase { get; set; } = string.Empty;
}

