using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

public class Campaign
{
    [JsonPropertyName("campaignId")]
    public int CampaignId { get; set; }

    [JsonPropertyName("campaignName")]
    public string CampaignName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("brandId")]
    public int BrandId { get; set; }

    [JsonPropertyName("brand")]
    public Brand? Brand { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("campaignProducts")]
    public List<CampaignProduct> CampaignProducts { get; set; } = new();

    [JsonPropertyName("questionConfigurations")]
    public List<CampaignQuestionConfig> QuestionConfigurations { get; set; } = new();
}