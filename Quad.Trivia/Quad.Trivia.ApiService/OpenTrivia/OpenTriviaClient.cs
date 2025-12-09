namespace Quad.Trivia.ApiService.OpenTrivia
{
    public class OpenTriviaClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<OpenTriviaQuestion> GetQuestionAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetFromJsonAsync<OpenTriviaResponse>("api.php?amount=1", cancellationToken);

            return response?.Results?.FirstOrDefault() ?? throw new InvalidOperationException("No question returned from OpenTivia API.");
        }
    }
}