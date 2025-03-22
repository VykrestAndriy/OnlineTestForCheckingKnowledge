using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Business.Services;
using AutoMapper;

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

        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            var tests = await _testService.GetAllTestsAsync();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _testService.GetTestByIdAsync(id);
            if (test == null) return NotFound();
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] TestDto testDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTest = await _testService.CreateTestAsync(testDto);
            return CreatedAtAction(nameof(GetTestById), new { id = createdTest.Id }, createdTest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] TestDto testDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTest = await _testService.UpdateTestAsync(id, testDto);
            if (updatedTest == null) return NotFound();
            return Ok(updatedTest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var result = await _testService.DeleteTestAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
