using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the repository for extracted product data.
/// </summary>
public class ExtractedProductDataRepository : IExtractedProductDataRepository
{
    private readonly ApplicationDbContext _context;

    public ExtractedProductDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExtractedProductData> AddProductDataAsync(ExtractedProductData productData)
    {
        await _context.ExtractedProductData.AddAsync(productData);
        await _context.SaveChangesAsync();
        return productData;
    }
}