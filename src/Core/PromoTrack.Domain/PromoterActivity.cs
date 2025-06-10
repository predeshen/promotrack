namespace PromoTrack.Domain;

public class PromoterActivity
{
    public int PromoterActivityId { get; set; }
    public int UserId { get; set; }
    public int CampaignId { get; set; }
    public int StoreId { get; set; }
    public DateTime? CheckInTimestamp { get; set; }
    public decimal? CheckInLatitude { get; set; }
    public decimal? CheckInLongitude { get; set; }
    public DateTime? CheckOutTimestamp { get; set; }
    public decimal? CheckOutLatitude { get; set; }
    public decimal? CheckOutLongitude { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public User? User { get; set; }
    public Campaign? Campaign { get; set; }
    public Store? Store { get; set; }
    public ICollection<ShelfImage> ShelfImages { get; set; } = new List<ShelfImage>();
    public ICollection<ExtractedProductData> ProductData { get; set; } = new List<ExtractedProductData>();

    /// <summary>
    /// Collection of survey answers submitted during this activity.
    /// </summary>
    public ICollection<SurveyAnswer> SurveyAnswers { get; set; } = new List<SurveyAnswer>();
}