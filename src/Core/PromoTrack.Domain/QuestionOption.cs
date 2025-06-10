namespace PromoTrack.Domain;

/// <summary>
/// Represents a single predefined option for a multiple-choice question.
/// </summary>
public class QuestionOption
{
    public int QuestionOptionId { get; set; }

    /// <summary>
    /// The text for this answer option (e.g., "Yes", "No", "500ml Coke").
    /// </summary>
    public string OptionText { get; set; } = string.Empty;

    /// <summary>
    /// The sort order for displaying this option in a list.
    /// </summary>
    public int SortOrder { get; set; } = 0;

    // --- Foreign Key & Navigation Property ---

    /// <summary>
    /// Foreign key to the parent Question.
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Navigation property to the parent Question.
    /// </summary>
    public Question? Question { get; set; }
}