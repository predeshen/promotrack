using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for data access operations related to Brand entities.
/// </summary>
public interface IBrandRepository
{
    Task<Brand?> GetBrandByIdAsync(int id);
    Task<IEnumerable<Brand>> GetAllBrandsAsync();
    Task<Brand> AddBrandAsync(Brand brand);
    Task UpdateBrandAsync(Brand brand);
    Task DeleteBrandAsync(int id);
    Task<BrandQuestionDefault> AddDefaultQuestionToBrandAsync(int brandId, int questionId, bool isMandatory, int sortOrder);

}