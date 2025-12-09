using Quad.Trivia.ApiService.Models;

namespace Quad.Trivia.ApiService.Repositories
{
    public interface ITriviaRepository
    {
        Task AddQuestionAsync(QuestionModel question, CancellationToken cancellationToken);

        Task<IEnumerable<QuestionModel>> GetAllQuestionsAsync(CancellationToken cancellationToken);

        Task<QuestionModel> GetQuestionAsync(Guid questionId, CancellationToken cancellationToken);

        Task<bool> QuestionExistsAsync(Guid questionId, CancellationToken cancellationToken);

        Task RemoveQuestionAsync(Guid questionId, CancellationToken cancellationToken);
    }
}