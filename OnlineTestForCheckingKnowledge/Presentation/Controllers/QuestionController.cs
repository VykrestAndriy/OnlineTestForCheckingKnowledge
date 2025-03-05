using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Business.DTOs;
using OnlineTestForCheckingKnowledge.Business.Services;
using OnlineTestForCheckingKnowledge.Data.Entities;
using AutoMapper;

namespace OnlineTestForCheckingKnowledge.Presentation.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = _mapper.Map<Question>(questionDto);
            var createdQuestion = await _questionService.CreateQuestionAsync(question);
            var createdQuestionDto = _mapper.Map<QuestionDto>(createdQuestion);

            return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.Id }, createdQuestionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = _mapper.Map<Question>(questionDto);
            var updatedQuestion = await _questionService.UpdateQuestionAsync(id, question);
            if (updatedQuestion == null) return NotFound();

            var updatedQuestionDto = _mapper.Map<QuestionDto>(updatedQuestion);
            return Ok(updatedQuestionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

