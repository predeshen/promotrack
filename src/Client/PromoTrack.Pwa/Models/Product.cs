using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

/// <summary>
/// Represents a Product object as received from the API.
/// </summary>
public class Product
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("defaultPrice")]
    public decimal? DefaultPrice { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}