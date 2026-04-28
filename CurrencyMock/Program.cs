using KK;

class Program
{
    static async Task Main()
    {
        // локальный Mountebank
        string url = "http://localhost:4545";
        using HttpClient client = new();
        var service = new CurrencyService(client, url);

        Console.WriteLine("Запрашиваем курс валют у Mock-сервиса...");

        try
        {
            var response = await service.GetRateAsync();
            if (response != null)
            {
                Console.WriteLine($"Валюта: {response.Currency}");
                Console.WriteLine($"Курс: {response.Rate} руб.");
                Console.WriteLine($"Дата обновления: {response.Date}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}