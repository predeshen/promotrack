using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for data access operations related to Store entities.
/// </summary>
public interface IStoreRepository
{
    Task<Store?> GetStoreByIdAsync(int id);
    Task<IEnumerable<Store>> GetAllStoresAsync();
    Task<Store> AddStoreAsync(Store store);
    Task UpdateStoreAsync(Store store);
    Task DeleteStoreAsync(int id);
}