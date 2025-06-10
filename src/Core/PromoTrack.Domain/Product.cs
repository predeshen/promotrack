namespace PromoTrack.Domain;
public class Product
{
    public int ProductId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? DefaultPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();
}