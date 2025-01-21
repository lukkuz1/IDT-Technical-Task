using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;

namespace QuizAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighScoresController : ControllerBase
    {
        private readonly QuizContext _context;

        public HighScoresController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/highscores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetTopScores(int page = 1, int pageSize = 10)
        {
            var highScores = await _context.HighScores
                .OrderByDescending(h => h.Score)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = highScores.Select((score, index) => new
            {
                Position = index + 1 + (page - 1) * pageSize,
                score.Email,
                score.Score,
                score.DateTime,
                Color = index == 0 ? "Gold" : index == 1 ? "Silver" : index == 2 ? "Bronze" : null
            });

            return Ok(result);
        }
    }
}