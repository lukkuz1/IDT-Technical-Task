using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext for in-memory database
builder.Services.AddDbContext<QuizContext>(options =>
    options.UseInMemoryDatabase("QuizDb"));

// Add controllers for the API
builder.Services.AddControllers();

// Add OpenAPI support (optional, for API documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add any other services, like if you'd like to add dependency injection, here

var app = builder.Build();

// Seed quiz data (optional, if you want to pre-populate the database)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuizContext>();
    if (!context.Quizzes.Any())
    {
        context.Quizzes.AddRange(new Quiz[]
        {
            new Quiz
            {
                Question = "What is the capital of France?",
                Type = QuestionType.Radio,
                Options = new List<Option>
                {
                    new Option { Text = "Paris", IsCorrect = true },
                    new Option { Text = "London", IsCorrect = false },
                    new Option { Text = "Berlin", IsCorrect = false }
                }
            },
            // Add more questions as needed
        });

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline (for development, add Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Map the controllers to their respective routes

app.Run();