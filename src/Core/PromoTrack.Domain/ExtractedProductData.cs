namespace PromoTrack.Domain;

/// <summary>
/// Stores product data captured during a promoter visit,
/// which can be from AI extraction or manual entry.
/// </summary>
public class ExtractedProductData
{
    public int ExtractedProductDataId { get; set; }

    // --- Foreign Key & Navigation ---
    public int PromoterActivityId { get; set; }
    public PromoterActivity? PromoterActivity { get; set; }

    // This links to the specific product from the campaign's product list
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    // --- Data Points ---

    /// <summary>
    /// The number of items observed on the shelf.
    /// </summary>
    public int? QuantityOnShelf { get; set; }

    /// <summary>
    /// The price of the product as seen on the shelf.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// The number of items sold during the promotion period.
    /// </summary>
    public int? ItemsSold { get; set; }

    /// <summary>
    /// Any other observations about this specific product.
    /// </summary>
    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; }
}