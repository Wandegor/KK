using System.Text.Json;
using RestSharp;

namespace ApiTest;

public class ShopApiTests
{
    private const string BaseUrl = "http://shop2.qatl.ru/shop/api";
    private RestClient _client;
    private List<string> _productsToDelete;
    
    [SetUp]
    public void Setup()
    {
        _client = new RestClient(BaseUrl);
        _productsToDelete = [];
    }

    [TearDown]
    public async Task TearDown()
    {
        foreach (var id in _productsToDelete)
        {
            var request = new RestRequest($"deleteproduct?id={id}", Method.Get);
            await _client.ExecuteAsync(request);
        }
        
        _client.Dispose();
    }

    private void AssertProductsAreEqual(ProductModel expected, ProductModel actual)
    {
        Assert.Multiple(() =>
            {
                Assert.That(actual.CategoryId, Is.EqualTo(expected.CategoryId), "Category mismatch");
                Assert.That(actual.Title, Is.EqualTo(expected.Title), "Title mismatch");
                Assert.That(actual.Alias, Is.EqualTo(expected.Alias), "Alias mismatch");
                Assert.That(actual.Content, Is.EqualTo(expected.Content), "Content mismatch");
                Assert.That(actual.Price, Is.EqualTo(expected.Price), "Price mismatch");
                Assert.That(actual.OldPrice, Is.EqualTo(expected.OldPrice), "OldPrice mismatch");
                Assert.That(actual.Status, Is.EqualTo(expected.Status), "Status mismatch");
                Assert.That(actual.Keywords, Is.EqualTo(expected.Keywords), "Keywords mismatch");
                Assert.That(actual.Description, Is.EqualTo(expected.Description), "Description mismatch");
                Assert.That(actual.Hit, Is.EqualTo(expected.Hit), "Hit mismatch");
                Assert.That(actual.Cat, Is.EqualTo(expected.Cat), "Cat mismatch");
            }
        );
    }
    
    [Test]
    public async Task GetProducts_ShouldReturnProductsList()
    {
        var request = new RestRequest("products", Method.Get);

        var response = await _client.ExecuteAsync(request);

        Console.WriteLine($"Status Code: {response.StatusCode}");
        Console.WriteLine($"Content Length: {response.Content?.Length}");

        Assert.That(response.IsSuccessful, Is.True, $"Запрос завершился с ошибкой. Status Code: {response.StatusCode}");

        Assert.That(response.Content, Is.Not.Null, "Ответ от сервера пустой");

        var products = JsonSerializer.Deserialize<List<ProductModel>>(response.Content);

        Assert.That(products, Is.Not.Null, "Не удалось спарсить JSON в список товаров!");
        
        Console.WriteLine($"Получено товаров: {products.Count}");

        if (products.Count <= 0)
        {
            Assert.Fail("Кол-во товаров <= 0");
        }
    }
    
    [TestCase(1, "Test Product 1", "InitTestPlsWork", 100.5, 1)]
    public async Task AddProduct_ShouldCreateProductSuccessfully(int categoryId, string title, string content, double price, int status)
    {
        var newProduct = new ProductModel
        {
            Id = new Random().Next(80000, 99999).ToString(),
            CategoryId = "6", // тоже строка
            Title = title,
            Alias = title.ToLower().Replace(" ", "-"),
            Content = "",
            Price = price.ToString(),
            OldPrice = "0",
            Status = "1",
            Keywords = "",
            Description = "",
            Img = "p-1.png",
            Hit = "1",
            Cat = "Casio"
        };
        
        _productsToDelete.Add(newProduct.Id);
        
        // Создание
        var addRequest = new RestRequest($"addproduct", Method.Post);
        addRequest.AddJsonBody(newProduct);
        var addResponse = await _client.ExecuteAsync(addRequest);
        
        Assert.That(addResponse.IsSuccessful, Is.True, "POST /addproduct failed");
        
        // Получение всеx товаров
        var getRequest = new RestRequest($"products", Method.Get);
        var getResponse = await _client.ExecuteAsync(getRequest);
        
        Console.WriteLine($"GET Status Code: {getResponse.StatusCode}");
        Console.WriteLine($"GET Content: {getResponse.Content}");
            
        Assert.That(getResponse.IsSuccessful, Is.True, $"GET /products failed with status {getResponse.StatusCode}");
        
        // Json -> список объектов
        var products = JsonSerializer.Deserialize<List<ProductModel>>(getResponse.Content);
        
        // Поиск моего
        var createdProduct = products.FirstOrDefault(p => p.Id == newProduct.Id);
        
        Assert.That(createdProduct, Is.Not.Null, "Created product was not found in the DB");
        
        AssertProductsAreEqual(newProduct, createdProduct);
    }
}