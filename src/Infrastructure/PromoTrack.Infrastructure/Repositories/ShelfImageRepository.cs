using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the repository for shelf images.
/// </summary>
public class ShelfImageRepository : IShelfImageRepository
{
    private readonly ApplicationDbContext _context;

    public ShelfImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShelfImage> AddImageAsync(ShelfImage image)
    {
        await _context.ShelfImages.AddAsync(image);
        await _context.SaveChangesAsync();
        return image;
    }
}