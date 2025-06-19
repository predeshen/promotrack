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
        // 1. Fetch the core campaign details, including its brand and any SPECIFIC question overrides.
        var campaign = await _context.Campaigns
            .Include(c => c.Brand)
            .Include(c => c.CampaignProducts)
                .ThenInclude(cp => cp.Product)
            .Include(c => c.QuestionConfigurations)
                .ThenInclude(qc => qc.Question)
            .AsNoTracking() // Use AsNoTracking for a read-only query for better performance.
            .FirstOrDefaultAsync(c => c.CampaignId == id);

        if (campaign == null)
        {
            return null;
        }

        // 2. Fetch the brand's default questions separately.
        var brandDefaultQuestions = await _context.BrandQuestionDefaults
            .Where(bqd => bqd.BrandId == campaign.BrandId)
            .Include(bqd => bqd.Question)
            .ToListAsync();

        // 3. MERGE LOGIC: Combine the lists to create the effective list of questions.

        var effectiveQuestionConfigs = new List<CampaignQuestionConfig>();
        var campaignOverrideQuestionIds = new HashSet<int>(campaign.QuestionConfigurations.Select(qc => qc.QuestionId));

        // First, add all the brand defaults, mapping them to the CampaignQuestionConfig structure.
        foreach (var brandDefault in brandDefaultQuestions)
        {
            effectiveQuestionConfigs.Add(new CampaignQuestionConfig
            {
                CampaignId = campaign.CampaignId,
                QuestionId = brandDefault.QuestionId,
                IsActiveForCampaign = true, // Defaults to active
                IsMandatoryForCampaign = brandDefault.IsMandatoryByDefault,
                SortOrderForCampaign = brandDefault.SortOrder,
                Question = brandDefault.Question
            });
        }

        // Now, process the campaign-specific overrides.
        foreach (var campaignConfig in campaign.QuestionConfigurations)
        {
            var existingConfig = effectiveQuestionConfigs.FirstOrDefault(ec => ec.QuestionId == campaignConfig.QuestionId);
            if (existingConfig != null)
            {
                // If the question exists from a brand default, UPDATE it with the campaign's specific settings.
                existingConfig.IsActiveForCampaign = campaignConfig.IsActiveForCampaign;
                existingConfig.IsMandatoryForCampaign = campaignConfig.IsMandatoryForCampaign;
                existingConfig.SortOrderForCampaign = campaignConfig.SortOrderForCampaign;
            }
            else
            {
                // If this is a new question added only at the campaign level, ADD it to the list.
                effectiveQuestionConfigs.Add(campaignConfig);
            }
        }

        // 4. Replace the campaign's original (potentially incomplete) list with our new, complete list.
        campaign.QuestionConfigurations = effectiveQuestionConfigs;

        return campaign;
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

    public async Task AssignPromoterToCampaignAsync(int campaignId, int userId)
    {
        // Check if the assignment already exists to prevent duplicates
        var exists = await _context.CampaignPromoters
            .AnyAsync(cp => cp.CampaignId == campaignId && cp.UserId == userId);

        if (!exists)
        {
            var campaignPromoter = new CampaignPromoter
            {
                CampaignId = campaignId,
                UserId = userId
            };
            await _context.CampaignPromoters.AddAsync(campaignPromoter);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Campaign>> GetCampaignsByPromoterIdAsync(int userId)
    {
        return await _context.CampaignPromoters
            .Where(cp => cp.UserId == userId)
            .Select(cp => cp.Campaign) // Select the Campaign navigation property
            .Include(c => c.Brand)     // Also include the Brand details
            .ToListAsync();
    }
}