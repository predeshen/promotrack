using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

public class CampaignProduct
{
    [JsonPropertyName("campaignId")]
    public int CampaignId { get; set; }

    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("product")]
    public Product? Product { get; set; }
}