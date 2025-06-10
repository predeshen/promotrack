using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Dtos;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/activities")] // Changed route for simplicity
public class PromoterActivityController : ControllerBase
{
    private readonly IPromoterActivityRepository _activityRepository;
    private readonly IShelfImageRepository _imageRepository; // <-- ADD THIS
    private readonly IExtractedProductDataRepository _productDataRepository; // <-- ADD THIS

    // Inject the new repository
    public PromoterActivityController(
        IPromoterActivityRepository activityRepository,
        IShelfImageRepository imageRepository,
        IExtractedProductDataRepository productDataRepository) // <-- ADD THIS
    {
        _activityRepository = activityRepository;
        _imageRepository = imageRepository;
        _productDataRepository = productDataRepository; // <-- ADD THIS
    }

    // ... existing check-in and check-out methods ...
    [HttpPost("check-in")]
    public async Task<ActionResult<PromoterActivity>> CheckIn([FromBody] CheckInDto checkInDto)
    {
        if (checkInDto == null) return BadRequest();
        var existingActivity = await _activityRepository.GetActiveActivityByUserIdAsync(checkInDto.UserId);
        if (existingActivity != null)
        {
            return Conflict("This user is already checked in.");
        }
        var activity = new PromoterActivity
        {
            UserId = checkInDto.UserId,
            CampaignId = checkInDto.CampaignId,
            StoreId = checkInDto.StoreId,
            CheckInTimestamp = DateTime.UtcNow,
            CheckInLatitude = checkInDto.Latitude,
            CheckInLongitude = checkInDto.Longitude,
            Status = "CheckedIn"
        };
        var newActivity = await _activityRepository.CheckInAsync(activity);
        return Ok(newActivity);
    }

    [HttpPost("check-out")]
    public async Task<ActionResult<PromoterActivity>> CheckOut([FromBody] CheckOutDto checkOutDto)
    {
        if (checkOutDto == null) return BadRequest();
        var activityToUpdate = await _activityRepository.GetActiveActivityByUserIdAsync(checkOutDto.UserId);
        if (activityToUpdate == null)
        {
            return NotFound("No active check-in found for this user.");
        }
        activityToUpdate.CheckOutTimestamp = DateTime.UtcNow;
        activityToUpdate.CheckOutLatitude = checkOutDto.Latitude;
        activityToUpdate.CheckOutLongitude = checkOutDto.Longitude;
        activityToUpdate.Notes = checkOutDto.Notes;
        activityToUpdate.Status = "Completed";
        var updatedActivity = await _activityRepository.CheckOutAsync(activityToUpdate);
        return Ok(updatedActivity);
    }

    /// <summary>
    /// Adds a shelf image to an existing activity.
    /// </summary>
    /// <param name="activityId">The ID of the promoter's activity/visit.</param>
    /// <param name="dto">The image data.</param>
    [HttpPost("{activityId}/images")]
    public async Task<ActionResult<ShelfImage>> AddImageToActivity(int activityId, [FromBody] AddImageDto dto)
    {
        var image = new ShelfImage
        {
            PromoterActivityId = activityId,
            ImageUrl = dto.ImageUrl,
            Caption = dto.Caption,
            UploadTimestamp = DateTime.UtcNow
        };

        var newImage = await _imageRepository.AddImageAsync(image);

        return Ok(newImage);
    }

    [HttpPost("{activityId}/product-data")]
    public async Task<ActionResult<ExtractedProductData>> AddProductDataToActivity(int activityId, [FromBody] AddProductDataDto dto)
    {
        var productData = new ExtractedProductData
        {
            PromoterActivityId = activityId,
            ProductId = dto.ProductId,
            QuantityOnShelf = dto.QuantityOnShelf,
            Price = dto.Price,
            ItemsSold = dto.ItemsSold,
            Notes = dto.Notes,
            CreatedDate = DateTime.UtcNow
        };

        var newData = await _productDataRepository.AddProductDataAsync(productData);

        return Ok(newData);
    }

    [HttpPost("{activityId}/answers")]
    public async Task<ActionResult<SurveyAnswer>> SubmitAnswer(int activityId, [FromBody] SubmitAnswerDto dto)
    {
        var answer = new SurveyAnswer
        {
            PromoterActivityId = activityId,
            QuestionId = dto.QuestionId,
            AnswerText = dto.AnswerText,
            AnswerTimestamp = DateTime.UtcNow
        };

        if (dto.SelectedOptionIds != null && dto.SelectedOptionIds.Any())
        {
            foreach (var optionId in dto.SelectedOptionIds)
            {
                answer.SelectedOptions.Add(new SurveyAnswerSelectedOption { QuestionOptionId = optionId });
            }
        }

        var newAnswer = await _activityRepository.AddSurveyAnswerAsync(answer);
        return Ok(newAnswer);
    }
}