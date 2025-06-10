using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

public interface IPromoterActivityRepository
{
    Task<PromoterActivity> CheckInAsync(PromoterActivity activity);
    Task<PromoterActivity?> GetActiveActivityByUserIdAsync(int userId);
    Task<PromoterActivity> CheckOutAsync(PromoterActivity activity);

    /// <summary>
    /// Adds a survey answer to a specific promoter activity.
    /// </summary>
    Task<SurveyAnswer> AddSurveyAnswerAsync(SurveyAnswer answer);
}