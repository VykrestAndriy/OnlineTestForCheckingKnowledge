using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;
using OnlineTestForCheckingKnowledge.Infrastructure.Repositories.Base; 
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OnlineTestForCheckingKnowledge.Infrastructure.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IRepository<Question>
    {
        private readonly AppDbContext _dbContext;

        public QuestionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Question>> GetQuestionsByTestIdAsync(int testId)
        {
            return await _dbContext.Questions
                .Where(q => q.TestId == testId)
                .ToListAsync();
        }
        public async Task<Question> GetQuestionWithAnswersAsync(int questionId)
        {
            return await _dbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }
    }
}