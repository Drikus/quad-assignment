namespace Quad.Trivia.ApiService.DTOs
{
    public class QuestionDTO
    {
        public Guid QuestionId { get; set; }

        public string Question { get; set; }

        public List<string> PossibleAnswers { get; set; }

        public string Category { get; set; }

        public string Difficulty { get; set; }

        public string Type { get; set; }
    }
}