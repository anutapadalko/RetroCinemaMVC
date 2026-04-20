using RetroCinemaDomain.Model;

namespace RetroCinemaInfrastructure.Services
{
    public interface IImportService<TEntity> where TEntity : Entity
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}