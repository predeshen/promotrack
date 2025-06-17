using System.Text.Json.Serialization;

namespace PromoTrack.Pwa.Models;

public class BrandQuestionDefault
{
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("isMandatoryByDefault")]
    public bool IsMandatoryByDefault { get; set; }

    [JsonPropertyName("sortOrder")]
    public int SortOrder { get; set; }

    [JsonPropertyName("question")]
    public Question? Question { get; set; }
}