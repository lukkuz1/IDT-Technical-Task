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
        public async Task<ActionResult<IEnumerable<HighScore>>> GetTopScores()
        {
            var topScores = await _context.HighScores
                .OrderByDescending(h => h.Score)
                .Take(10)
                .ToListAsync();

            return topScores;
        }
    }
}
