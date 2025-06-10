namespace PromoTrack.Domain;

/// <summary>
/// Represents a single image of a shelf uploaded by a promoter during a visit.
/// </summary>
public class ShelfImage
{
    public int ShelfImageId { get; set; }

    /// <summary>
    /// The URL pointing to the stored image.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// An optional caption for the image.
    /// </summary>
    public string? Caption { get; set; }

    public DateTime UploadTimestamp { get; set; }

    // --- Foreign Key & Navigation Property ---

    /// <summary>
    /// Foreign key to the PromoterActivity this image belongs to.
    /// </summary>
    public int PromoterActivityId { get; set; }

    /// <summary>
    /// Navigation property to the parent PromoterActivity.
    /// </summary>
    public PromoterActivity? PromoterActivity { get; set; }
}