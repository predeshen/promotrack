namespace PromoTrack.Application.Dtos;

/// <summary>
/// DTO for a promoter check-in request.
/// </summary>
public class CheckInDto
{
    public int UserId { get; set; }
    public int CampaignId { get; set; }
    public int StoreId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}