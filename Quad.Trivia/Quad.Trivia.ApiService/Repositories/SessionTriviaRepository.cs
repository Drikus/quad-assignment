using Quad.Trivia.ApiService.Models;

namespace Quad.Trivia.ApiService.Repositories
{
    public class SessionTriviaRepository : ITriviaRepository
    {
        private readonly List<QuestionModel> _questions = new();

        public async Task<QuestionModel> GetQuestionAsync(Guid questionId, CancellationToken cancellationToken = default)
        {
            var result = _questions.FirstOrDefault(q => q.QuestionId == questionId);

            return result ?? throw new KeyNotFoundException($"Question with ID {questionId} not found.");
        }

        public async Task AddQuestionAsync(QuestionModel question, CancellationToken cancellationToken = default) => _questions.Add(question);

        public async Task<IEnumerable<QuestionModel>> GetAllQuestionsAsync(CancellationToken cancellationToken = default) => _questions;

        public async Task<bool> QuestionExistsAsync(Guid questionId, CancellationToken cancellationToken = default) => _questions.Any(q => q.QuestionId == questionId);

        public async Task RemoveQuestionAsync(Guid questionId, CancellationToken cancellationToken = default)
        {
            var question = _questions.FirstOrDefault(q => q.QuestionId == questionId);
            if (question != null)
            {
                _questions.Remove(question);
            }
        }
    }
}