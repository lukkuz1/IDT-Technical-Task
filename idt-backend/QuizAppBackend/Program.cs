using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<QuizContext>(options =>
    options.UseInMemoryDatabase("QuizDb"));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuizContext>();
    

    context.Database.EnsureCreated();

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
            new Quiz
            {
                Question = "Select the colors in the French flag.",
                Type = QuestionType.Checkbox,
                Options = new List<Option>
                {
                    new Option { Text = "Blue", IsCorrect = true },
                    new Option { Text = "White", IsCorrect = true },
                    new Option { Text = "Red", IsCorrect = true },
                    new Option { Text = "Green", IsCorrect = false }
                }
            },
            new Quiz
            {
                Question = "What is the chemical symbol for water?",
                Type = QuestionType.Textbox,
                Options = new List<Option>()
            }
        });
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();