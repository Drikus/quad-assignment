using System.Text.Json.Serialization;

namespace Quad.Trivia.ApiService.OpenTrivia
{
    public class OpenTriviaResponse
    {
        [JsonPropertyName("response_code")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("results")]
        public List<OpenTriviaQuestion>? Results { get; set; }
    }
}