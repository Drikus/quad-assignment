using Quad.Trivia.ApiService.DTOs;
using System.Text.Json.Serialization;

namespace Quad.Trivia.Web;

public class TriviaApiClient(HttpClient httpClient)
{
    public async Task<QuestionDTO?> GetQuestionAsync(CancellationToken cancellationToken = default) => await httpClient.GetFromJsonAsync<QuestionDTO>("/questions", cancellationToken);

    public async Task<QuestionDTO?> GetQuestionAsync(Guid questionId, CancellationToken cancellationToken = default) => await httpClient.GetFromJsonAsync<QuestionDTO>($"/questions/{questionId}", cancellationToken);

    public async Task<bool> CheckAnswerAsync(Guid questionId, string userAnswer, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsync($"/CheckAnswers/{questionId}", new StringContent(userAnswer), cancellationToken);

        var result = await response.Content.ReadAsStringAsync();
        return string.Equals(result, "true", StringComparison.OrdinalIgnoreCase);
    }
}
