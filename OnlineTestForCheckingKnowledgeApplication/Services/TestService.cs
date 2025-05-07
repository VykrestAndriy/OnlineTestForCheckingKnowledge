using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestForCheckingKnowledge.Business.Services
{
    public class TestService : ITestService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TestService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test?> GetTestByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public Test GetTestById(int id)
        {
            return _context.Tests
                .Where(t => t.Id == id)
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();
        }

        public async Task<Test> CreateTestAsync(TestDto testDto)
        {
            var test = _mapper.Map<Test>(testDto);
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> UpdateTestAsync(int id, TestDto testDto)
        {
            var existingTest = await _context.Tests.FindAsync(id);
            if (existingTest == null)
            {
                return null;
            }

            _mapper.Map(testDto, existingTest);
            await _context.SaveChangesAsync();
            return existingTest;
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return false;
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Test> CreateTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> UpdateTestAsync(int id, Test test)
        {
            var existingTest = await _context.Tests.FindAsync(id);
            if (existingTest == null)
            {
                return null;
            }

            existingTest.Title = test.Title;
            await _context.SaveChangesAsync();
            return existingTest;
        }

        public void CreateMultipleTests(int numberOfTests)
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                var newTest = new Test
                {
                    Title = $"Тест {i + 1}",
                    Name = $"Тест {i + 1}",
                    Questions = new List<Question>()
                };

                for (int j = 1; j <= 5; j++) // Створення 5 питань для кожного тесту
                {
                    var newQuestion = new Question
                    {
                        Text = $"Запитання №{j} до тесту {i + 1}?",
                        Test = newTest,
                        Answers = new List<Answer>()
                        {
                            new Answer { Text = $"Варіант А до питання {j}", IsCorrect = (j == 1) }, // Перша відповідь робимо правильною для прикладу
                            new Answer { Text = $"Варіант Б до питання {j}", IsCorrect = false },
                            new Answer { Text = $"Варіант В до питання {j}", IsCorrect = false }
                        }
                    };
                    newTest.Questions.Add(newQuestion);
                    _context.Questions.Add(newQuestion);
                }
                _context.Tests.Add(newTest);
            }
            _context.SaveChanges();
        }

        public async Task DeleteAllTestsAsync()
        {
            await _context.Tests.ExecuteDeleteAsync();
        }
    }
}