namespace PromoTrack.Domain;

/// <summary>
/// Represents a physical store location where promotions take place.
/// </summary>
public class Store
{
    /// <summary>
    /// The unique identifier for the store. Primary Key.
    /// </summary>
    public int StoreId { get; set; }

    /// <summary>
    /// The name of the store (e.g., "Checkers Queensburgh").
    /// </summary>
    public string StoreName { get; set; } = string.Empty;

    /// <summary>
    /// An optional unique code for the store.
    /// </summary>
    public string? StoreCode { get; set; }

    /// <summary>
    /// The first line of the store's address.
    /// </summary>
    public string? AddressLine1 { get; set; }

    /// <summary>
    /// The second line of the store's address.
    /// </summary>
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// The city where the store is located.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// The province where the store is located.
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// The postal code for the store's address.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// The GPS latitude of the store for mapping and check-ins.
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// The GPS longitude of the store for mapping and check-ins.
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// A flag to indicate if the store is currently active for promotions.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The date and time when the store was added to the system.
    /// </summary>
    public DateTime CreatedDate { get; set; }
}