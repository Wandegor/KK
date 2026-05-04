using KK;

namespace CurrencyMock.Tests;

public class UnitTest1
{
    private const string BaseUrl = "http://localhost:4545";
    
    [Fact]
    public async Task GetRateNullAsyncTest()
    {
        using HttpClient httpClient = new();
        CurrencyService service = new(httpClient, BaseUrl);
        
        CurrencyResponse? response = await service.GetRateAsync(null);
        
        Assert.NotNull(response);
        Assert.Equal("USD", response.Currency);
        Assert.Equal(92.45m, response.Rate);
        Assert.Equal("28-04-2026", response.Date);
    }
    
    [Fact]
    public async Task GetRateAsync_RUB()
    {
        using var httpClient = new HttpClient();
        var service = new CurrencyService(httpClient, BaseUrl);

        var response = await service.GetRateAsync("RUB");

        Assert.NotNull(response);
        Assert.Equal("RUB", response!.Currency);
        Assert.Equal(100.00m, response.Rate);
        Assert.Equal("20-04-2025", response.Date);
    }
    
    [Fact]
    public async Task GetRateAsync_USD()
    {
        using var httpClient = new HttpClient();
        var service = new CurrencyService(httpClient, BaseUrl);

        var response = await service.GetRateAsync("USD");

        Assert.NotNull(response);
        Assert.Equal("USD", response.Currency);
        Assert.Equal(92.45m, response.Rate);
        Assert.Equal("28-04-2026", response.Date);
    }
    
    [Fact]
    public async Task GetRateAsync_NotValidCurrency()
    {
        using var httpClient = new HttpClient();
        var service = new CurrencyService(httpClient, BaseUrl);

        await Assert.ThrowsAsync<HttpRequestException>(() => service.GetRateAsync("UDBRA"));
    }
}