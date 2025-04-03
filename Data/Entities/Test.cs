namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new();
    }
}
