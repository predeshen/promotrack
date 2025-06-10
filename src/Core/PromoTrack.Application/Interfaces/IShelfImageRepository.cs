using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for shelf image data operations.
/// </summary>
public interface IShelfImageRepository
{
    Task<ShelfImage> AddImageAsync(ShelfImage image);
}