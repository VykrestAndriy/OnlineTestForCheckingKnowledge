using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Business.Services;
using AutoMapper;
using OnlineTestForCheckingKnowledge.ViewModels;
using System.Linq;

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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTests(int numberOfTests = 5)
        {
            var allExistingTests = _testService.GetAllTestsAsync().Result;
            foreach (var test in allExistingTests)
            {
                _testService.DeleteTestAsync(test.Id).Wait();
            }

            _testService.CreateMultipleTests(numberOfTests);
            return RedirectToAction("SelectListOfTests");
        }

        public IActionResult SelectListOfTests()
        {
            var allTests = _testService.GetAllTestsAsync().Result;
            return View(allTests);
        }

        [HttpGet("StartTest/{testId:int}")]
        public IActionResult StartTest(int testId)
        {
            var test = _testService.GetTestById(testId);

            if (test == null || test.Questions == null || !test.Questions.Any())
            {
                TempData["ErrorMessage"] = "Test not found or does not contain questions.";
                return RedirectToAction("SelectListOfTests");
            }

            var viewModel = new TestViewModel
            {
                TestName = test.Title,
                Questions = test.Questions.ToList(),
                CurrentQuestionIndex = 0,
                UserAnswers = new Dictionary<int, int>()
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

            if (model.Questions != null && model.CurrentQuestionIndex < model.Questions.Count - 1)
            {
                model.CurrentQuestionIndex++;
                return View("DisplayQuestion", model);
            }
            else if (model.Questions != null && model.CurrentQuestionIndex == model.Questions.Count - 1)
            {
                return RedirectToAction("Index", "Home"); // Переконайтеся, що "Home" - це назва вашого головного контролера
            }
            else
            {
                return RedirectToAction("SelectListOfTests");
            }
        }

        public IActionResult TestResult(Dictionary<int, int> userAnswers)
        {
            var testId = 1;
            var test = _testService.GetTestById(testId);

            if (test?.Questions == null || !test.Questions.Any())
            {
                return View("Error");
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
            ViewBag.TestName = test.Name;

            return View(userAnswers);
        }
    }
}