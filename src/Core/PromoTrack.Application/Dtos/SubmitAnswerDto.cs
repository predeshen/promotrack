namespace PromoTrack.Application.Dtos;

public class SubmitAnswerDto
{
    public int QuestionId { get; set; }

    // For FreeText, Numeric, Yes/No answers
    public string? AnswerText { get; set; }

    // For MultipleChoice answers
    public List<int>? SelectedOptionIds { get; set; }
}