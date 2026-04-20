using RetroCinemaDomain.Model;

namespace RetroCinemaInfrastructure.Services
{
    public class GenreDataPortServiceFactory : IDataPortServiceFactory<Genre>
    {
        private readonly RetroCinemaDbContext _context;

        public GenreDataPortServiceFactory(RetroCinemaDbContext context)
        {
            _context = context;
        }

        public IImportService<Genre> GetImportService(string contentType)
        {
            if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                return new GenreImportService(_context);
            throw new NotImplementedException($"No import service implemented for content type {contentType}");
        }

        public IExportService<Genre> GetExportService(string contentType)
        {
            if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                return new GenreExportService(_context);
            throw new NotImplementedException($"No export service implemented for content type {contentType}");
        }
    }
}