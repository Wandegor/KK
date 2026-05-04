using System.Net.Http.Json;

namespace KK;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public CurrencyService(HttpClient httpClient, string baseUrl)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/');
    }

    public async Task<CurrencyResponse?> GetRateAsync(string? currency)
    {
        var url = $"{_baseUrl}/api/rate";
        if (!string.IsNullOrEmpty(currency))
        {
            url+= $"?currency={currency}";
        }
        return await _httpClient.GetFromJsonAsync<CurrencyResponse>(url);
    }
}