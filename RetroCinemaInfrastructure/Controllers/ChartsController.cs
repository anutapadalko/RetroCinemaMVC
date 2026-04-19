using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroCinemaDomain.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace RetroCinemaInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly RetroCinemaDbContext _context;

        public record ChartData(string Label, int Count);

        public ChartsController(RetroCinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet("moviesByYear")]
        public async Task<JsonResult> GetMoviesByYearAsync(CancellationToken cancellationToken)
        {
            var data = await _context.Movies
                .Where(m => m.ReleaseYear != null)
                .GroupBy(m => m.ReleaseYear)
                .Select(g => new ChartData(g.Key.ToString()!, g.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(data);
        }

        [HttpGet("sessionsByHall")]
        public async Task<JsonResult> GetSessionsByHallAsync(CancellationToken cancellationToken)
        {
            var data = await _context.Sessions
                .Include(s => s.Hall)
                .Where(s => s.Hall != null)
                .GroupBy(s => s.Hall.Name)
                .Select(g => new ChartData(g.Key, g.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(data);
        }
    }
}