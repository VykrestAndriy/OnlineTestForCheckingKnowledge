using OnlineTestForCheckingKnowledge.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<Question> CreateQuestionAsync(Question question);
        Task<Question?> UpdateQuestionAsync(int id, Question question);
        Task<bool> DeleteQuestionAsync(int id);
    }
}