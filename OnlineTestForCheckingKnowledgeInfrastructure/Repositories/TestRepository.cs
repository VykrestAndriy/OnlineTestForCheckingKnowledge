using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineTestForCheckingKnowledge.Infrastructure.Repositories
{
    public class TestRepository : IRepository<Test>
    {
        private readonly AppDbContext _context;

        public TestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<IEnumerable<Test>> FindAsync(Expression<Func<Test, bool>> predicate)
        {
            return await _context.Tests.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Test entity)
        {
            await _context.Tests.AddAsync(entity);
        }

        public void Update(Test entity)
        {
            _context.Tests.Update(entity);
        }

        public void Remove(Test entity)
        {
            _context.Tests.Remove(entity);
        }

        public async Task<int> SaveChangesAsync() 
        {
            return await _context.SaveChangesAsync();
        }
    }
}