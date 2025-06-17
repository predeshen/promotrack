using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

/// <summary>
/// A lightweight model for populating brand selection dropdowns.
/// </summary>
public class BrandSelectionModel
{
    [JsonPropertyName("brandId")]
    public int BrandId { get; set; }

    [JsonPropertyName("brandName")]
    public string BrandName { get; set; } = string.Empty;
}