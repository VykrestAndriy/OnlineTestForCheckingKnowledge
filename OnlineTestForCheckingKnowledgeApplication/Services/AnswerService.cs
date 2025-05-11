using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Data.Entities;
using AutoMapper;
using OnlineTestForCheckingKnowledge.Data;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AnswerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Answer>> GetAllAnswersAsync()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<Answer?> GetAnswerByIdAsync(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<Answer> CreateAnswerAsync(AnswerDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<Answer?> UpdateAnswerAsync(int id, AnswerDto answerDto)
        {
            var existingAnswer = await _context.Answers.FindAsync(id);
            if (existingAnswer == null)
            {
                return null;
            }

            _mapper.Map(answerDto, existingAnswer);
            await _context.SaveChangesAsync();
            return existingAnswer;
        }

        public async Task<bool> DeleteAnswerAsync(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return false;
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
