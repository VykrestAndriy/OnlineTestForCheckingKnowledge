using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;
using System.Linq.Expressions;

namespace OnlineTestForCheckingKnowledge.Infrastructure.Repositories
{
    public class AnswerRepository : IRepository<Answer>
    {
        private readonly AppDbContext _context;

        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Answer?> GetByIdAsync(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<IEnumerable<Answer>> FindAsync(Expression<Func<Answer, bool>> predicate)
        {
            return await _context.Answers.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Answer entity)
        {
            await _context.Answers.AddAsync(entity);
        }

        public void Update(Answer entity)
        {
            _context.Answers.Update(entity);
        }

        public void Remove(Answer entity)
        {
            _context.Answers.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
