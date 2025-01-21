using QuizAppBackend.Data;
using QuizAppBackend.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace QuizAppBackend
{
    public static class QuizDataSeeder
    {
        public static void SeedData(QuizContext context)
        {
            if (!context.Quizzes.Any())
            {
                context.Quizzes.AddRange(
                    // Radio Questions (5 total)
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
                        Question = "Which of the following is a valid C# data type?",
                        Type = QuestionType.Radio,
                        Options = new List<Option>
                        {
                            new Option { Text = "int", IsCorrect = true },
                            new Option { Text = "integer", IsCorrect = false },
                            new Option { Text = "float32", IsCorrect = false }
                        }
                    },
                    new Quiz
                    {
                        Question = "Which of these is a fruit?",
                        Type = QuestionType.Radio,
                        Options = new List<Option>
                        {
                            new Option { Text = "Apple", IsCorrect = true },
                            new Option { Text = "Carrot", IsCorrect = false },
                            new Option { Text = "Broccoli", IsCorrect = false }
                        }
                    },
                    new Quiz
                    {
                        Question = "What is the largest planet in our solar system?",
                        Type = QuestionType.Radio,
                        Options = new List<Option>
                        {
                            new Option { Text = "Jupiter", IsCorrect = true },
                            new Option { Text = "Saturn", IsCorrect = false },
                            new Option { Text = "Earth", IsCorrect = false }
                        }
                    },
                    new Quiz
                    {
                        Question = "Which is the longest river in the world?",
                        Type = QuestionType.Radio,
                        Options = new List<Option>
                        {
                            new Option { Text = "Amazon River", IsCorrect = true },
                            new Option { Text = "Nile River", IsCorrect = false },
                            new Option { Text = "Yangtze River", IsCorrect = false }
                        }
                    },

                    // Checkbox Questions (5 total)
                    new Quiz
                    {
                        Question = "Which of the following are programming languages?",
                        Type = QuestionType.Checkbox,
                        Options = new List<Option>
                        {
                            new Option { Text = "C#", IsCorrect = true },
                            new Option { Text = "Python", IsCorrect = true },
                            new Option { Text = "Microsoft Word", IsCorrect = false },
                            new Option { Text = "JavaScript", IsCorrect = true }
                        }
                    },
                    new Quiz
                    {
                        Question = "Which of the following are web browsers?",
                        Type = QuestionType.Checkbox,
                        Options = new List<Option>
                        {
                            new Option { Text = "Google Chrome", IsCorrect = true },
                            new Option { Text = "Mozilla Firefox", IsCorrect = true },
                            new Option { Text = "VLC Media Player", IsCorrect = false },
                            new Option { Text = "Safari", IsCorrect = true }
                        }
                    },
                    new Quiz
                    {
                        Question = "Which of these are social media platforms?",
                        Type = QuestionType.Checkbox,
                        Options = new List<Option>
                        {
                            new Option { Text = "Facebook", IsCorrect = true },
                            new Option { Text = "Instagram", IsCorrect = true },
                            new Option { Text = "Reddit", IsCorrect = true },
                            new Option { Text = "Spotify", IsCorrect = false }
                        }
                    },
                    new Quiz
                    {
                        Question = "Which of the following are types of operating systems?",
                        Type = QuestionType.Checkbox,
                        Options = new List<Option>
                        {
                            new Option { Text = "Windows", IsCorrect = true },
                            new Option { Text = "MacOS", IsCorrect = true },
                            new Option { Text = "Linux", IsCorrect = true },
                            new Option { Text = "WordPress", IsCorrect = false }
                        }
                    },
                    new Quiz
                    {
                        Question = "Which of the following are musical instruments?",
                        Type = QuestionType.Checkbox,
                        Options = new List<Option>
                        {
                            new Option { Text = "Piano", IsCorrect = true },
                            new Option { Text = "Violin", IsCorrect = true },
                            new Option { Text = "Trumpet", IsCorrect = true },
                            new Option { Text = "Cell phone", IsCorrect = false }
                        }
                    },

                    // Textbox Questions (5 total)
                    new Quiz
                    {
                        Question = "What is your favorite color?",
                        Type = QuestionType.Textbox,
                        Options = new List<Option>()
                    },
                    new Quiz
                    {
                        Question = "Describe your ideal vacation destination.",
                        Type = QuestionType.Textbox,
                        Options = new List<Option>()
                    },
                    new Quiz
                    {
                        Question = "Who is your favorite historical figure?",
                        Type = QuestionType.Textbox,
                        Options = new List<Option>()
                    },
                    new Quiz
                    {
                        Question = "What is the most challenging problem you've solved?",
                        Type = QuestionType.Textbox,
                        Options = new List<Option>()
                    },
                    new Quiz
                    {
                        Question = "If you could meet anyone in the world, who would it be?",
                        Type = QuestionType.Textbox,
                        Options = new List<Option>()
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
