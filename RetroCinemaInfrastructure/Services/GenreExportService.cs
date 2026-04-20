using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using RetroCinemaDomain.Model;

namespace RetroCinemaInfrastructure.Services
{
    public class GenreExportService : IExportService<Genre>
    {
        private readonly RetroCinemaDbContext _context;

        public GenreExportService(RetroCinemaDbContext context)
        {
            _context = context;
        }

        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanWrite) throw new ArgumentException("Потік не доступний для запису");

            var movies = await _context.Movies.ToListAsync(cancellationToken);
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Всі фільми");

            worksheet.Cell(1, 1).Value = "Назва фільму";
            worksheet.Cell(1, 2).Value = "Опис";
            worksheet.Cell(1, 3).Value = "Рік випуску";
            worksheet.Cell(1, 4).Value = "Режисер";
            worksheet.Cell(1, 5).Value = "Тривалість (хв)";
            worksheet.Row(1).Style.Font.Bold = true;

            int rowIndex = 2;
            foreach (var movie in movies)
            {
                worksheet.Cell(rowIndex, 1).Value = movie.Title;
                worksheet.Cell(rowIndex, 2).Value = movie.Description;
                worksheet.Cell(rowIndex, 3).Value = movie.ReleaseYear;
                worksheet.Cell(rowIndex, 4).Value = movie.Director;
                worksheet.Cell(rowIndex, 5).Value = movie.DurationMinutes; 
                rowIndex++;
            }

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(stream);
        }
    }
}