using System.Text.Json.Serialization;

namespace ApiTest;

public class ProductModel
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("category_id")] public string CategoryId { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("alias")] public string Alias { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("price")] public string Price { get; set; }
    [JsonPropertyName("old_price")] public string OldPrice { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("keywords")] public string Keywords { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("img")] public string Img { get; set; }
    [JsonPropertyName("hit")] public string Hit { get; set; }
    [JsonPropertyName("cat")] public string Cat { get; set; }
}