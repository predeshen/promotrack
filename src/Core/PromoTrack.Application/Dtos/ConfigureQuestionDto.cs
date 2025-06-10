namespace PromoTrack.Application.Dtos;

public class ConfigureQuestionDto
{
    public int QuestionId { get; set; }
    public bool IsActiveForCampaign { get; set; } = true;
    public bool IsMandatoryForCampaign { get; set; } = false;
    public int SortOrderForCampaign { get; set; } = 0;
}