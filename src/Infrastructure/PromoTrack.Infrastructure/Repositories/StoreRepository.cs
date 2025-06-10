using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the IStoreRepository interface using Entity Framework Core.
/// </summary>
public class StoreRepository : IStoreRepository
{
    private readonly ApplicationDbContext _context;

    public StoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Store?> GetStoreByIdAsync(int id)
    {
        return await _context.Stores.FindAsync(id);
    }

    public async Task<IEnumerable<Store>> GetAllStoresAsync()
    {
        return await _context.Stores.ToListAsync();
    }

    public async Task<Store> AddStoreAsync(Store store)
    {
        await _context.Stores.AddAsync(store);
        await _context.SaveChangesAsync();
        return store;
    }

    public async Task UpdateStoreAsync(Store store)
    {
        _context.Stores.Update(store);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStoreAsync(int id)
    {
        var storeToDelete = await _context.Stores.FindAsync(id);
        if (storeToDelete != null)
        {
            _context.Stores.Remove(storeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}