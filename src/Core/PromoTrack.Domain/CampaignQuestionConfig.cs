namespace PromoTrack.Domain;

/// <summary>
/// Configures a specific question for a single campaign,
/// overriding any brand defaults.
/// </summary>
public class CampaignQuestionConfig
{
    public int CampaignQuestionConfigId { get; set; }

    // --- Foreign Keys ---
    public int CampaignId { get; set; }
    public int QuestionId { get; set; }

    // --- Override Properties ---
    public bool IsActiveForCampaign { get; set; } = true;
    public bool IsMandatoryForCampaign { get; set; } = false;
    public int SortOrderForCampaign { get; set; } = 0;

    // --- Navigation Properties ---
    public Campaign? Campaign { get; set; }
    public Question? Question { get; set; }
}