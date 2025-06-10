using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IStoreRepository _storeRepository;

    public StoresController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Store>>> GetAllStores()
    {
        var stores = await _storeRepository.GetAllStoresAsync();
        return Ok(stores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Store>> GetStoreById(int id)
    {
        var store = await _storeRepository.GetStoreByIdAsync(id);
        if (store == null) return NotFound();
        return Ok(store);
    }

    [HttpPost]
    public async Task<ActionResult<Store>> CreateStore(Store store)
    {
        store.CreatedDate = DateTime.UtcNow;
        var newStore = await _storeRepository.AddStoreAsync(store);
        return CreatedAtAction(nameof(GetStoreById), new { id = newStore.StoreId }, newStore);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStore(int id, Store store)
    {
        if (id != store.StoreId)
        {
            return BadRequest();
        }

        var storeToUpdate = await _storeRepository.GetStoreByIdAsync(id);
        if (storeToUpdate == null)
        {
            return NotFound();
        }

        // In a real app, use AutoMapper or a mapping function
        storeToUpdate.StoreName = store.StoreName;
        storeToUpdate.StoreCode = store.StoreCode;
        storeToUpdate.AddressLine1 = store.AddressLine1;
        storeToUpdate.AddressLine2 = store.AddressLine2;
        storeToUpdate.City = store.City;
        storeToUpdate.Province = store.Province;
        storeToUpdate.PostalCode = store.PostalCode;
        storeToUpdate.Latitude = store.Latitude;
        storeToUpdate.Longitude = store.Longitude;
        storeToUpdate.IsActive = store.IsActive;

        await _storeRepository.UpdateStoreAsync(storeToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
        var storeToDelete = await _storeRepository.GetStoreByIdAsync(id);
        if (storeToDelete == null)
        {
            return NotFound();
        }

        await _storeRepository.DeleteStoreAsync(id);
        return NoContent();
    }
}