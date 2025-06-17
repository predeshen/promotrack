using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

public class CampaignQuestionConfig
{
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("isActiveForCampaign")]
    public bool IsActiveForCampaign { get; set; }

    [JsonPropertyName("isMandatoryForCampaign")]
    public bool IsMandatoryForCampaign { get; set; }

    [JsonPropertyName("sortOrderForCampaign")]
    public int SortOrderForCampaign { get; set; }

    [JsonPropertyName("question")]
    public Question? Question { get; set; }
}