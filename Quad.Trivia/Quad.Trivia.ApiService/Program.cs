using Quad.Trivia.ApiService;
using Quad.Trivia.ApiService.OpenTrivia;
using Quad.Trivia.ApiService.Repositories;
using Quad.Trivia.ApiService.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<OpenTriviaClient>(client =>
{
    client.BaseAddress = new("https+http://opentdb.com");
});

builder.Services.AddTransient<ITriviaService, TriviaService>();
builder.Services.AddSingleton<ITriviaRepository, SessionTriviaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Map trivia endpoints from extension
app.MapTriviaEndpoints();

app.MapDefaultEndpoints();

app.Run();