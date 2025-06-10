namespace PromoTrack.Application.Dtos;

public class AddDefaultQuestionDto
{
    public int QuestionId { get; set; }
    public bool IsMandatoryByDefault { get; set; } = false;
    public int SortOrder { get; set; } = 0;
}