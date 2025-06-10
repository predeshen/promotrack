namespace PromoTrack.Application.Dtos;

public class AddProductDataDto
{
    public int ProductId { get; set; }
    public int? QuantityOnShelf { get; set; }
    public decimal? Price { get; set; }
    public int? ItemsSold { get; set; }
    public string? Notes { get; set; }
}