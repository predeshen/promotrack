using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

/// <summary>
/// Represents a Brand object as received from the API.
/// This is a client-side model.
/// </summary>
public class Brand
{
    [JsonPropertyName("brandId")]
    public int BrandId { get; set; }

    [JsonPropertyName("brandName")]
    public string BrandName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    [JsonPropertyName("defaultQuestions")]
    public List<BrandQuestionDefault> DefaultQuestions { get; set; } = new();
}