using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Business.Services;
using AutoMapper;

namespace OnlineTestForCheckingKnowledge.Presentation.Controllers
{
    [Route("api/answers")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;

        public AnswerController(IAnswerService answerService, IMapper mapper)
        {
            _answerService = answerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            var answers = await _answerService.GetAllAnswersAsync();
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null) return NotFound();
            return Ok(answer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerDto answerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAnswer = await _answerService.CreateAnswerAsync(answerDto);
            return CreatedAtAction(nameof(GetAnswerById), new { id = createdAnswer.Id }, createdAnswer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, [FromBody] AnswerDto answerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedAnswer = await _answerService.UpdateAnswerAsync(id, answerDto);
            if (updatedAnswer == null) return NotFound();
            return Ok(updatedAnswer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var result = await _answerService.DeleteAnswerAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}