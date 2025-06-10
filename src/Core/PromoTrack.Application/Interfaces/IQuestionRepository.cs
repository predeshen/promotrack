using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for data access operations related to Questions and QuestionOptions.
/// </summary>
public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetAllQuestionsAsync();
    Task<Question?> GetQuestionByIdAsync(int id);
    Task<Question> AddQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(int id);
}