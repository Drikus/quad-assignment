namespace Quad.Trivia.ApiService.Models
{
    public class QuestionModel
    {
        public Guid QuestionId { get; set; }

        public string Question { get; set; }

        public string Category { get; set; }

        public QuestionDifficulty Difficulty { get; set; }

        public QuestionType Type { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> PossibleAnswers { get; set; }
    }
}