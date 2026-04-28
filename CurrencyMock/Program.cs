using System.Net.Http.Json;
using KK;

class Program
{
    static async Task Main(string[] args)
    {
        // локальный Mountebank
        string url = "http://localhost:4545/api/rate";
        
        using var client = new HttpClient();

        Console.WriteLine("Запрашиваем курс валют у Mock-сервиса...");

        try 
        {
            // Отправляем запрос
            var response = await client.GetFromJsonAsync<CurrencyResponse>(url);

            if (response != null)
            {
                Console.WriteLine("--- Данные получены успешно ---");
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