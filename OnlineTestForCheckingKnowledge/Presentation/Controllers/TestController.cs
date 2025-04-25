using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Business.Services;
using AutoMapper;
using OnlineTestForCheckingKnowledge.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace OnlineTestForCheckingKnowledge.Presentation.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        public IActionResult StartTest(int testId)
        {
            var test = _testService.GetTestById(testId);

            if (test == null || test.Questions == null || !test.Questions.Any())
            {
                return NotFound();
            }

            var viewModel = new TestViewModel
            {
                TestName = test.Name,
                Questions = test.Questions.ToList(),
                CurrentQuestionIndex = 0
            };

            return View("DisplayQuestion", viewModel);
        }

        [HttpPost]
        public IActionResult NextQuestion(TestViewModel model, int? selectedAnswerId)
        {
            if (model.CurrentQuestion != null && selectedAnswerId.HasValue)
            {
                model.UserAnswers[model.CurrentQuestion.Id] = selectedAnswerId.Value;
            }

            model.CurrentQuestionIndex++;

            if (model.Questions != null && model.CurrentQuestionIndex < model.Questions.Count)
            {
                return View("DisplayQuestion", model);
            }
            else
            {
                return RedirectToAction("TestResult", model.UserAnswers);
            }
        }

        public IActionResult TestResult(Dictionary<int, int> userAnswers)
        {
            var testId = 1; // Вам потрібно буде якось передавати ID тесту
            var test = _testService.GetTestById(testId);

            if (test?.Questions == null || !test.Questions.Any())
            {
                return View("Error"); // Або інша обробка помилки
            }

            int correctAnswers = 0;
            foreach (var question in test.Questions)
            {
                if (userAnswers.ContainsKey(question.Id) && question.CorrectAnswerId == userAnswers[question.Id])
                {
                    correctAnswers++;
                }
            }

            ViewBag.TotalQuestions = test.Questions.Count;
            ViewBag.CorrectAnswers = correctAnswers;
            ViewBag.Percentage = (double)correctAnswers / test.Questions.Count * 100;
            ViewBag.TestName = test.Name; // Передаємо назву тесту для відображення

            return View(userAnswers);
        }
    }
}