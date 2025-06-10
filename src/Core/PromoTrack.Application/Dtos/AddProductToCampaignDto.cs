namespace PromoTrack.Application.Dtos;

public class AddProductToCampaignDto
{
    public int ProductId { get; set; }
    public decimal? CampaignSpecificPrice { get; set; }
}