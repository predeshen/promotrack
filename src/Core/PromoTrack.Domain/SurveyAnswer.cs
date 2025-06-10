namespace PromoTrack.Domain;

/// <summary>
/// Stores a promoter's answer to a specific campaign question during an activity.
/// </summary>
public class SurveyAnswer
{
    public int SurveyAnswerId { get; set; }

    // --- Foreign Keys ---
    public int PromoterActivityId { get; set; }
    public int QuestionId { get; set; } // Storing QuestionId directly for easier reporting

    // --- Answer Data ---
    public string? AnswerText { get; set; } // For FreeText, Numeric, YesNo, etc.

    public DateTime AnswerTimestamp { get; set; }

    // --- Navigation Properties ---
    public PromoterActivity? PromoterActivity { get; set; }
    public Question? Question { get; set; }

    /// <summary>
    /// For multiple-choice questions, this will hold the selected options.
    /// </summary>
    public ICollection<SurveyAnswerSelectedOption> SelectedOptions { get; set; } = new List<SurveyAnswerSelectedOption>();
}