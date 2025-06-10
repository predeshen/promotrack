namespace PromoTrack.Domain;

public class Campaign
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();

    /// <summary>
    /// Collection of specific question configurations for this campaign.
    /// </summary>
    public ICollection<CampaignQuestionConfig> QuestionConfigurations { get; set; } = new List<CampaignQuestionConfig>();
}