using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using RetroCinemaDomain.Model;
using System.Collections.Generic;
using System.Linq;

namespace RetroCinemaInfrastructure.Services
{
    public class GenreImportService : IImportService<Genre>
    {
        private readonly RetroCinemaDbContext _context;

        public GenreImportService(RetroCinemaDbContext context)
        {
            _context = context;
        }

        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanRead) throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));

            using (XLWorkbook workBook = new XLWorkbook(stream))
            {
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    var genreName = worksheet.Name;

                    var genre = await _context.Genres
                        .Include(g => g.Movies)
                        .FirstOrDefaultAsync(g => g.Name == genreName, cancellationToken);

                    if (genre == null)
                    {
                        genre = new Genre { Name = genreName };
                        _context.Genres.Add(genre);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        await AddMovieAsync(row, cancellationToken, genre);
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task AddMovieAsync(IXLRow row, CancellationToken cancellationToken, Genre genre)
        {
            var movieTitle = row.Cell(1).Value.ToString();

            var movie = new Movie
            {
                Title = movieTitle
            };

            if (genre.Movies == null)
            {
                genre.Movies = new List<Movie>();
            }

            if (!genre.Movies.Any(m => m.Title == movieTitle))
            {
                genre.Movies.Add(movie);
                _context.Movies.Add(movie);
            }

            await Task.CompletedTask;
        }
    }
}