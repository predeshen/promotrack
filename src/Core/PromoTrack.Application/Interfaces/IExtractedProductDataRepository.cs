using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for product data operations.
/// </summary>
public interface IExtractedProductDataRepository
{
    Task<ExtractedProductData> AddProductDataAsync(ExtractedProductData productData);
}