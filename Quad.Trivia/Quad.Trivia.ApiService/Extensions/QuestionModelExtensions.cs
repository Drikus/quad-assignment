using Quad.Trivia.ApiService.DTOs;

namespace Quad.Trivia.ApiService.Extensions
{
    public static class QuestionModelExtensions
    {
        public static QuestionDTO ToDTO(this Models.QuestionModel questionModel)
        {
            var possibleAnswers = questionModel.PossibleAnswers.ToList() ?? [];

            possibleAnswers.Add(questionModel.CorrectAnswer);

            possibleAnswers = [.. possibleAnswers.OrderBy(x => Guid.NewGuid())];

            return new QuestionDTO
            {
                QuestionId = questionModel.QuestionId,
                Question = questionModel.Question,
                Category = questionModel.Category,
                Difficulty = questionModel.Difficulty.ToString(),
                Type = questionModel.Type.ToString(),
                PossibleAnswers = possibleAnswers
            };
        }
    }
}