using System.Collections.Generic;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.ViewModels
{
    public class TestViewModel
    {
        public string TestName { get; set; }
        public List<Question> Questions { get; set; }
        public int CurrentQuestionIndex { get; set; } = 0; 
        public Question CurrentQuestion => Questions != null && CurrentQuestionIndex < Questions.Count ? Questions[CurrentQuestionIndex] : null;
        public List<Answer> CurrentAnswers => CurrentQuestion?.Answers;
        public Dictionary<int, int> UserAnswers { get; set; } = new Dictionary<int, int>();
    }
}