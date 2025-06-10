namespace PromoTrack.Domain;

/// <summary>
/// Represents the default linking of a Question to a Brand,
/// with default properties for when a campaign is created for this brand.
/// </summary>
public class BrandQuestionDefault
{
    public int BrandQuestionDefaultId { get; set; }

    // --- Foreign Keys ---
    public int BrandId { get; set; }
    public int QuestionId { get; set; }

    // --- Default Properties ---
    public bool IsMandatoryByDefault { get; set; } = false;
    public int SortOrder { get; set; } = 0;

    // --- Navigation Properties ---
    public Brand? Brand { get; set; }
    public Question? Question { get; set; }
}