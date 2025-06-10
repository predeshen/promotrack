using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the IProductRepository interface using Entity Framework Core.
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var productToDelete = await _context.Products.FindAsync(id);
        if (productToDelete != null)
        {
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }
    }
}