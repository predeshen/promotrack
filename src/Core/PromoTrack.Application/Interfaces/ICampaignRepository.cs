using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

public interface ICampaignRepository
{
    Task<Campaign?> GetCampaignByIdAsync(int id);
    Task<IEnumerable<Campaign>> GetAllCampaignsAsync();
    Task<Campaign> AddCampaignAsync(Campaign campaign);
    Task UpdateCampaignAsync(Campaign campaign);
    Task DeleteCampaignAsync(int id);
    Task<CampaignProduct?> AddProductToCampaignAsync(int campaignId, int productId, decimal? specificPrice);

    /// <summary>
    /// Configures a question for a specific campaign, overriding brand defaults.
    /// </summary>
    Task<CampaignQuestionConfig> ConfigureQuestionForCampaignAsync(int campaignId, int questionId, bool isActive, bool isMandatory, int sortOrder);
}