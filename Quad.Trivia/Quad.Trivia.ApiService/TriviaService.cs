using Quad.Trivia.ApiService.Models;
using Quad.Trivia.ApiService.OpenTrivia;
using Quad.Trivia.ApiService.Repositories;

namespace Quad.Trivia.ApiService
{
    public class TriviaService(OpenTriviaClient openTriviaClient, ITriviaRepository triviaRepository) : ITriviaService
    {
        private readonly OpenTriviaClient _openTriviaClient = openTriviaClient;
        private readonly ITriviaRepository _triviaRepository = triviaRepository;

        public async Task<QuestionModel> GetNewTriviaQuestionAsync(CancellationToken cancellationToken = default)
        {
            var openTriviaResult = await _openTriviaClient.GetQuestionAsync(cancellationToken);
            if (openTriviaResult == null)
            {
                throw new Exception("Failed to retrieve trivia question from OpenTrivia.");
            }

            var questionModel = new QuestionModel
            {
                QuestionId = Guid.NewGuid(),
                Question = openTriviaResult.Question ?? "No question available",
                Category = openTriviaResult.Category ?? "Unknown",
                Difficulty = openTriviaResult.Difficulty switch
                {
                    "easy" => QuestionDifficulty.Easy,
                    "medium" => QuestionDifficulty.Medium,
                    "hard" => QuestionDifficulty.Hard,
                    _ => QuestionDifficulty.Unknown
                },
                Type = openTriviaResult.Type switch
                {
                    "multiple" => QuestionType.MultipleChoice,
                    "boolean" => QuestionType.TrueFalse,
                    _ => QuestionType.Unknown
                },
                CorrectAnswer = openTriviaResult.CorrectAnswer ?? "No answer available",
                PossibleAnswers = openTriviaResult.IncorrectAnswers ?? []
            };

            await _triviaRepository.AddQuestionAsync(questionModel, cancellationToken);

            return questionModel;
        }

        public async Task<QuestionModel> GetTriviaQuestionAsync(Guid questionId, CancellationToken cancellationToken = default) => await _triviaRepository.GetQuestionAsync(questionId, cancellationToken);

        public async Task<bool> CheckAnswerAsync(Guid questionId, string userAnswer, CancellationToken cancellationToken = default)
        {
            if (!await _triviaRepository.QuestionExistsAsync(questionId, cancellationToken))
            {
                return false;
            }

            var question = await GetTriviaQuestionAsync(questionId, cancellationToken);

            return string.Equals(question.CorrectAnswer, userAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }
}