using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the repository for promoter activities.
/// </summary>
public class PromoterActivityRepository : IPromoterActivityRepository
{
    private readonly ApplicationDbContext _context;

    public PromoterActivityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PromoterActivity> CheckInAsync(PromoterActivity activity)
    {
        await _context.PromoterActivities.AddAsync(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public async Task<PromoterActivity?> GetActiveActivityByUserIdAsync(int userId)
    {
        // Find the most recent activity for this user that is still in the "CheckedIn" status.
        return await _context.PromoterActivities
            .FirstOrDefaultAsync(a => a.UserId == userId && a.Status == "CheckedIn");
    }

    public async Task<PromoterActivity> CheckOutAsync(PromoterActivity activity)
    {
        _context.PromoterActivities.Update(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    public async Task<SurveyAnswer> AddSurveyAnswerAsync(SurveyAnswer answer)
    {
        await _context.SurveyAnswers.AddAsync(answer);
        await _context.SaveChangesAsync();
        return answer;
    }
}