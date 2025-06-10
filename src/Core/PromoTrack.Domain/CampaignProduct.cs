namespace PromoTrack.Domain;

/// <summary>
/// Represents the link between a Campaign and a Product.
/// This is a junction entity for the many-to-many relationship.
/// </summary>
public class CampaignProduct
{
    public int CampaignProductId { get; set; }
    public int CampaignId { get; set; }
    public int ProductId { get; set; }


    public Campaign? Campaign { get; set; }
    public Product? Product { get; set; }
    public decimal? CampaignSpecificPrice { get; set; }
}