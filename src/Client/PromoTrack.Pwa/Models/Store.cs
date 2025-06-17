using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

/// <summary>
/// Represents a Store object as received from the API.
/// </summary>
public class Store
{
    [JsonPropertyName("storeId")]
    public int StoreId { get; set; }

    [JsonPropertyName("storeName")]
    public string StoreName { get; set; } = string.Empty;

    [JsonPropertyName("storeCode")]
    public string? StoreCode { get; set; }

    [JsonPropertyName("addressLine1")]
    public string? AddressLine1 { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("province")]
    public string? Province { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}