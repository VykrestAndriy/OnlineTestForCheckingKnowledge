using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetAllAnswersAsync();
        Task<Answer?> GetAnswerByIdAsync(int id);
        Task<Answer> CreateAnswerAsync(AnswerDto answerDto);
        Task<Answer?> UpdateAnswerAsync(int id, AnswerDto answerDto);
        Task<bool> DeleteAnswerAsync(int id);
    }
}