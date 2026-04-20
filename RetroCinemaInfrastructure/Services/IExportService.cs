using RetroCinemaDomain.Model;

namespace RetroCinemaInfrastructure.Services
{
    public interface IExportService<TEntity> where TEntity : Entity
    {
        Task WriteToAsync(Stream stream, CancellationToken cancellationToken);
    }
}