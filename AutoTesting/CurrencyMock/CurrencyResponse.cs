using System.Text.Json.Serialization;

namespace KK;

public class CurrencyResponse
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }
    [JsonPropertyName("date")]
    public string Date { get; set; }
}