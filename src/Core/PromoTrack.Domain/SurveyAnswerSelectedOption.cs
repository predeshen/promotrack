namespace PromoTrack.Domain;

/// <summary>
/// Links a SurveyAnswer to a selected QuestionOption for multiple-choice answers.
/// </summary>
public class SurveyAnswerSelectedOption
{
    public int SurveyAnswerSelectedOptionId { get; set; }

    // --- Foreign Keys ---
    public int SurveyAnswerId { get; set; }
    public int QuestionOptionId { get; set; }

    // --- Navigation Properties ---
    public SurveyAnswer? SurveyAnswer { get; set; }
    public QuestionOption? QuestionOption { get; set; }
}