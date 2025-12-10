namespace Quad.Trivia.ApiService.Endpoints
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Quad.Trivia.ApiService;
    using Quad.Trivia.ApiService.Extensions;
    using System;
    using System.IO;
    using System.Threading;

    public static class TriviaEndpoints
    {
        public static WebApplication MapTriviaEndpoints(this WebApplication app)
        {
            app.MapGet("/questions", async (ITriviaService triviaService, CancellationToken cancellationToken) =>
            {
                var question = await triviaService.GetNewTriviaQuestionAsync(cancellationToken);
                return Results.Ok(question.ToDTO());
            })
            .WithName("GetQuestion");

            app.MapGet("/questions/{questionId:guid}", async (Guid questionId, ITriviaService triviaService, CancellationToken cancellationToken) =>
            {
                var question = await triviaService.GetTriviaQuestionAsync(questionId, cancellationToken);
                return Results.Ok(question.ToDTO());
            })
            .WithName("GetQuestionById");

            app.MapPost("CheckAnswers/{questionId:guid}", async (Guid questionId, HttpRequest request, ITriviaService triviaService, CancellationToken cancellationToken) =>
            {
                string userAnswer;

                using (var reader = new StreamReader(request.Body))
                {
                    userAnswer = await reader.ReadToEndAsync();
                }

                if (string.IsNullOrWhiteSpace(userAnswer))
                {
                    return Results.BadRequest("User answer cannot be empty.");
                }

                var isCorrect = await triviaService.CheckAnswerAsync(questionId, userAnswer.Trim(), cancellationToken);
                return Results.Ok(isCorrect);
            })
            .WithName("CheckAnswer");

            return app;
        }
    }
}