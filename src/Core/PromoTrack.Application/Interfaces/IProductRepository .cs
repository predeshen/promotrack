using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for data access operations related to Product entities.
/// </summary>
public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}