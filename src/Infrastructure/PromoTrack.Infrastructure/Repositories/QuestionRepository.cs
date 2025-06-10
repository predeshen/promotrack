using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the IQuestionRepository interface.
/// </summary>
public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;

    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        // Include the options for each question
        return await _context.Questions.Include(q => q.Options).ToListAsync();
    }

    public async Task<Question?> GetQuestionByIdAsync(int id)
    {
        return await _context.Questions.Include(q => q.Options).FirstOrDefaultAsync(q => q.QuestionId == id);
    }

    public async Task<Question> AddQuestionAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task UpdateQuestionAsync(Question question)
    {
        // This is a more complex update, as options might have been added/removed/changed.
        // We need to fetch the existing question from the database first.
        var existingQuestion = await _context.Questions.Include(q => q.Options).FirstOrDefaultAsync(q => q.QuestionId == question.QuestionId);

        if (existingQuestion != null)
        {
            // Update parent question properties
            _context.Entry(existingQuestion).CurrentValues.SetValues(question);

            // Remove options that are no longer in the updated list
            foreach (var existingOption in existingQuestion.Options.ToList())
            {
                if (!question.Options.Any(o => o.QuestionOptionId == existingOption.QuestionOptionId))
                    _context.QuestionOptions.Remove(existingOption);
            }

            // Add or update options
            foreach (var updatedOption in question.Options)
            {
                var existingOption = existingQuestion.Options.FirstOrDefault(o => o.QuestionOptionId == updatedOption.QuestionOptionId && updatedOption.QuestionOptionId != 0);

                if (existingOption != null)
                    // Update existing option
                    _context.Entry(existingOption).CurrentValues.SetValues(updatedOption);
                else
                    // Add new option
                    existingQuestion.Options.Add(updatedOption);
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteQuestionAsync(int id)
    {
        var questionToDelete = await _context.Questions.FindAsync(id);
        if (questionToDelete != null)
        {
            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync();
        }
    }
}