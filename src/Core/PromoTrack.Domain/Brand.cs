namespace PromoTrack.Domain;

public class Brand
{
    public int BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Collection of default questions associated with this brand.
    /// </summary>
    public ICollection<BrandQuestionDefault> DefaultQuestions { get; set; } = new List<BrandQuestionDefault>();
}