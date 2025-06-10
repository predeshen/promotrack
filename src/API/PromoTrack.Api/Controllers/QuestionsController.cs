using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionsController(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions()
    {
        var questions = await _questionRepository.GetAllQuestionsAsync();
        return Ok(questions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestionById(int id)
    {
        var question = await _questionRepository.GetQuestionByIdAsync(id);
        if (question == null) return NotFound();
        return Ok(question);
    }

    [HttpPost]
    public async Task<ActionResult<Question>> CreateQuestion(Question question)
    {
        question.CreatedDate = DateTime.UtcNow;
        var newQuestion = await _questionRepository.AddQuestionAsync(question);
        return CreatedAtAction(nameof(GetQuestionById), new { id = newQuestion.QuestionId }, newQuestion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(int id, Question question)
    {
        if (id != question.QuestionId)
        {
            return BadRequest();
        }
        await _questionRepository.UpdateQuestionAsync(question);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var questionToDelete = await _questionRepository.GetQuestionByIdAsync(id);
        if (questionToDelete == null)
        {
            return NotFound();
        }
        await _questionRepository.DeleteQuestionAsync(id);
        return NoContent();
    }
}