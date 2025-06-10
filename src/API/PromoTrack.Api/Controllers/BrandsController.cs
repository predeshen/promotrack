using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Dtos;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandRepository _brandRepository;

    public BrandsController(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        var brands = await _brandRepository.GetAllBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrandById(int id)
    {
        var brand = await _brandRepository.GetBrandByIdAsync(id);
        if (brand == null) return NotFound();
        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<Brand>> CreateBrand(Brand brand)
    {
        // For simple entities like Brand, we can use the domain model directly
        // if there are no security concerns like passwords.
        brand.CreatedDate = DateTime.UtcNow;
        var newBrand = await _brandRepository.AddBrandAsync(brand);
        return CreatedAtAction(nameof(GetBrandById), new { id = newBrand.BrandId }, newBrand);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, Brand brand)
    {
        if (id != brand.BrandId)
        {
            return BadRequest();
        }

        var brandToUpdate = await _brandRepository.GetBrandByIdAsync(id);
        if (brandToUpdate == null)
        {
            return NotFound();
        }

        // We can use a mapper here in the future, but for now this is clear.
        brandToUpdate.BrandName = brand.BrandName;
        brandToUpdate.Description = brand.Description;
        brandToUpdate.IsActive = brand.IsActive;

        await _brandRepository.UpdateBrandAsync(brandToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var brandToDelete = await _brandRepository.GetBrandByIdAsync(id);
        if (brandToDelete == null)
        {
            return NotFound();
        }

        await _brandRepository.DeleteBrandAsync(id);
        return NoContent();
    }

    [HttpPost("{brandId}/default-questions")]
    public async Task<ActionResult<BrandQuestionDefault>> AddDefaultQuestion(int brandId, [FromBody] AddDefaultQuestionDto dto)
    {
        // In a real app, you'd verify both the brand and question exist first.
        var newDefault = await _brandRepository.AddDefaultQuestionToBrandAsync(
            brandId,
            dto.QuestionId,
            dto.IsMandatoryByDefault,
            dto.SortOrder);

        return Ok(newDefault);
    }
}