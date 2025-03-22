using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public class TestService : ITestService
    {
        private readonly AppDbContext _context;

        public TestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await _context.Tests.Include(t => t.Questions).ToListAsync();
        }

        public async Task<Test?> GetTestByIdAsync(int id)
        {
            return await _context.Tests.Include(t => t.Questions)
                                       .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Test> CreateTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> UpdateTestAsync(int id, Test updatedTest)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null) return null;

            test.Title = updatedTest.Title;
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null) return false;

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
