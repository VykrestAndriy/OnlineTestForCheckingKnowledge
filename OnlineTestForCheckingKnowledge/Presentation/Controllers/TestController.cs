using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Business.Services;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Presentation.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] TestDto testDto)
        {
            var test = _mapper.Map<Test>(testDto);
            var createdTest = await _testService.CreateTestAsync(test);
            return CreatedAtAction(nameof(GetTestById), new { id = createdTest.Id }, createdTest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _testService.GetTestByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

    }
}
