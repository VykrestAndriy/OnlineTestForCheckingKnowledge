using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineTestForCheckingKnowledge.Data.Entities;
using OnlineTestForCheckingKnowledge.Data;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(AppDbContext context, ILogger<QuestionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            try
            {
                return await _context.Questions.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all questions");
                throw;
            }
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            try
            {
                return await _context.Questions.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting question by id: {id}");
                throw;
            }
        }

        public async Task<Question> CreateQuestionAsync(Question question)
        {
            try
            {
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return question;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question");
                throw;
            }
        }

        public async Task<Question?> UpdateQuestionAsync(int id, Question question)
        {
            try
            {
                var existingQuestion = await _context.Questions.FindAsync(id);
                if (existingQuestion == null)
                {
                    return null;
                }

                existingQuestion.Text = question.Text;
                existingQuestion.TestId = question.TestId;

                await _context.SaveChangesAsync();
                return existingQuestion;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating question with id: {id}");
                throw;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            try
            {
                var question = await _context.Questions.FindAsync(id);
                if (question == null)
                {
                    return false;
                }

                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting question with id: {id}");
                throw;
            }
        }
    }
}
