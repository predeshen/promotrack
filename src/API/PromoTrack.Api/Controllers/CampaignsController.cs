using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Dtos;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly ICampaignRepository _campaignRepository;

    public CampaignsController(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetAllCampaigns()
    {
        var campaigns = await _campaignRepository.GetAllCampaignsAsync();
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Campaign>> GetCampaignById(int id)
    {
        var campaign = await _campaignRepository.GetCampaignByIdAsync(id);
        if (campaign == null) return NotFound();
        return Ok(campaign);
    }

    [HttpPost]
    public async Task<ActionResult<Campaign>> CreateCampaign(Campaign campaign)
    {
        campaign.CreatedDate = DateTime.UtcNow;
        var newCampaign = await _campaignRepository.AddCampaignAsync(campaign);
        return CreatedAtAction(nameof(GetCampaignById), new { id = newCampaign.CampaignId }, newCampaign);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCampaign(int id, Campaign campaign)
    {
        if (id != campaign.CampaignId)
        {
            return BadRequest();
        }

        var campaignToUpdate = await _campaignRepository.GetCampaignByIdAsync(id);
        if (campaignToUpdate == null)
        {
            return NotFound();
        }

        campaignToUpdate.CampaignName = campaign.CampaignName;
        campaignToUpdate.Description = campaign.Description;
        campaignToUpdate.StartDate = campaign.StartDate;
        campaignToUpdate.EndDate = campaign.EndDate;
        campaignToUpdate.BrandId = campaign.BrandId;
        campaignToUpdate.IsActive = campaign.IsActive;

        await _campaignRepository.UpdateCampaignAsync(campaignToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var campaignToDelete = await _campaignRepository.GetCampaignByIdAsync(id);
        if (campaignToDelete == null)
        {
            return NotFound();
        }

        await _campaignRepository.DeleteCampaignAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/products")]
    public async Task<ActionResult> AddProductToCampaign(int id, AddProductToCampaignDto dto)
    {
        var campaignProduct = await _campaignRepository.AddProductToCampaignAsync(id, dto.ProductId, dto.CampaignSpecificPrice);
        if (campaignProduct == null)
        {
            // This might happen if the campaign or product doesn't exist,
            // or if there's a constraint violation. We can add more specific checks later.
            return BadRequest("Could not add product to campaign.");
        }
        return Ok(campaignProduct);
    }

    /// </summary>
    [HttpPost("{campaignId}/questions")]
    public async Task<ActionResult<CampaignQuestionConfig>> ConfigureQuestionForCampaign(int campaignId, [FromBody] ConfigureQuestionDto dto)
    {
        var config = await _campaignRepository.ConfigureQuestionForCampaignAsync(
            campaignId,
            dto.QuestionId,
            dto.IsActiveForCampaign,
            dto.IsMandatoryForCampaign,
            dto.SortOrderForCampaign);

        return Ok(config);
    }
}