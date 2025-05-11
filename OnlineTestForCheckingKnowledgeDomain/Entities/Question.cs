using System.Collections.Generic;

namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class Question : IBaseEntity 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int? CorrectAnswerId { get; set; }
        public List<Answer> Answers { get; set; }
        public Answer CorrectAnswer { get; set; }
    }
}