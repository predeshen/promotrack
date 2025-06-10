using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        product.CreatedDate = DateTime.UtcNow;
        var newProduct = await _productRepository.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }

        var productToUpdate = await _productRepository.GetProductByIdAsync(id);
        if (productToUpdate == null)
        {
            return NotFound();
        }

        productToUpdate.Sku = product.Sku;
        productToUpdate.ProductName = product.ProductName;
        productToUpdate.Description = product.Description;
        productToUpdate.DefaultPrice = product.DefaultPrice;
        productToUpdate.IsActive = product.IsActive;

        await _productRepository.UpdateProductAsync(productToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var productToDelete = await _productRepository.GetProductByIdAsync(id);
        if (productToDelete == null)
        {
            return NotFound();
        }

        await _productRepository.DeleteProductAsync(id);
        return NoContent();
    }
}