using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the ICampaignRepository interface using Entity Framework Core.
/// </summary>
public class CampaignRepository : ICampaignRepository
{
    private readonly ApplicationDbContext _context;

    public CampaignRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Campaign?> GetCampaignByIdAsync(int id)
    {
        // Use Include to also load the related Brand data
        return await _context.Campaigns
            .Include(c => c.Brand)
            .FirstOrDefaultAsync(c => c.CampaignId == id);
    }

    public async Task<IEnumerable<Campaign>> GetAllCampaignsAsync()
    {
        // Use Include to also load the related Brand data for the list
        return await _context.Campaigns
            .Include(c => c.Brand)
            .ToListAsync();
    }

    public async Task<Campaign> AddCampaignAsync(Campaign campaign)
    {
        await _context.Campaigns.AddAsync(campaign);
        await _context.SaveChangesAsync();
        return campaign;
    }

    public async Task UpdateCampaignAsync(Campaign campaign)
    {
        _context.Campaigns.Update(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCampaignAsync(int id)
    {
        var campaignToDelete = await _context.Campaigns.FindAsync(id);
        if (campaignToDelete != null)
        {
            _context.Campaigns.Remove(campaignToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<CampaignProduct?> AddProductToCampaignAsync(int campaignId, int productId, decimal? specificPrice)
    {
        var campaignProduct = new CampaignProduct
        {
            CampaignId = campaignId,
            ProductId = productId,
            CampaignSpecificPrice = specificPrice
        };

        await _context.CampaignProducts.AddAsync(campaignProduct);
        await _context.SaveChangesAsync();
        return campaignProduct;
    }

    public async Task<CampaignQuestionConfig> ConfigureQuestionForCampaignAsync(int campaignId, int questionId, bool isActive, bool isMandatory, int sortOrder)
    {
        // Find if a configuration already exists
        var existingConfig = await _context.CampaignQuestionConfigs
            .FirstOrDefaultAsync(c => c.CampaignId == campaignId && c.QuestionId == questionId);

        if (existingConfig != null)
        {
            // Update existing configuration
            existingConfig.IsActiveForCampaign = isActive;
            existingConfig.IsMandatoryForCampaign = isMandatory;
            existingConfig.SortOrderForCampaign = sortOrder;
            _context.CampaignQuestionConfigs.Update(existingConfig);
        }
        else
        {
            // Create new configuration
            existingConfig = new CampaignQuestionConfig
            {
                CampaignId = campaignId,
                QuestionId = questionId,
                IsActiveForCampaign = isActive,
                IsMandatoryForCampaign = isMandatory,
                SortOrderForCampaign = sortOrder
            };
            await _context.CampaignQuestionConfigs.AddAsync(existingConfig);
        }

        await _context.SaveChangesAsync();
        return existingConfig;
    }
}