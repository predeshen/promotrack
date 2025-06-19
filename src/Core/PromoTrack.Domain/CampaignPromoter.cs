namespace PromoTrack.Domain;

/// <summary>
/// Represents the assignment of a User (Promoter) to a Campaign.
/// This is a junction entity.
/// </summary>
public class CampaignPromoter
{
    // --- Foreign Keys that form a composite primary key ---
    public int CampaignId { get; set; }
    public int UserId { get; set; }

    // --- Navigation Properties ---
    public Campaign? Campaign { get; set; }
    public User? User { get; set; }
}