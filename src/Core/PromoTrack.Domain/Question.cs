namespace PromoTrack.Domain;

public class Question
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string QuestionType { get; set; } = string.Empty;
    public string? HintText { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    public ICollection<BrandQuestionDefault> BrandDefaults { get; set; } = new List<BrandQuestionDefault>();

    /// <summary>
    /// Collection of campaign-specific configurations for this question.
    /// </summary>
    public ICollection<CampaignQuestionConfig> CampaignConfigurations { get; set; } = new List<CampaignQuestionConfig>();
}