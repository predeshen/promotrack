namespace PromoTrack.Application.Dtos;

/// <summary>
/// DTO for a promoter check-out request.
/// </summary>
public class CheckOutDto
{
    public int UserId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Notes { get; set; }
}