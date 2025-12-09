using Quad.Trivia.ApiService.Models;
using Quad.Trivia.ApiService.OpenTrivia;

namespace Quad.Trivia.ApiService
{
    public interface ITriviaService
    {
        Task<bool> CheckAnswerAsync(Guid questionId, string userAnswer, CancellationToken cancellationToken = default);

        Task<QuestionModel> GetNewTriviaQuestionAsync(CancellationToken cancellationToken = default);

        Task<QuestionModel> GetTriviaQuestionAsync(Guid questionId, CancellationToken cancellationToken = default);
    }
}