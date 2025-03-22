using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public interface ITestService
    {
        Task<List<Test>> GetAllTestsAsync();
        Task<Test?> GetTestByIdAsync(int id);
        Task<Test> CreateTestAsync(Test test);
        Task<Test?> UpdateTestAsync(int id, Test test);
        Task<bool> DeleteTestAsync(int id);
    }
}
