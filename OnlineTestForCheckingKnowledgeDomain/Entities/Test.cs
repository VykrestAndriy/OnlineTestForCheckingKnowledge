using OnlineTestForCheckingKnowledge.Data.Entities;
using System.Collections.Generic;

namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class Test : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new();
        public string Name { get; set; }
    }
}