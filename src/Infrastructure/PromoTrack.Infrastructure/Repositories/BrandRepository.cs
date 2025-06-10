using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the IBrandRepository interface using Entity Framework Core.
/// </summary>
public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Brand?> GetBrandByIdAsync(int id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand> AddBrandAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task UpdateBrandAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBrandAsync(int id)
    {
        var brandToDelete = await _context.Brands.FindAsync(id);
        if (brandToDelete != null)
        {
            _context.Brands.Remove(brandToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<BrandQuestionDefault> AddDefaultQuestionToBrandAsync(int brandId, int questionId, bool isMandatory, int sortOrder)
    {
        var defaultQuestion = new BrandQuestionDefault
        {
            BrandId = brandId,
            QuestionId = questionId,
            IsMandatoryByDefault = isMandatory,
            SortOrder = sortOrder
        };

        await _context.BrandQuestionDefaults.AddAsync(defaultQuestion);
        await _context.SaveChangesAsync();
        return defaultQuestion;
    }
}