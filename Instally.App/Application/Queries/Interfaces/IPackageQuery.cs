using Instally.App.Application.Entities;

namespace Instally.App.Application.Queries.Interfaces
{
    public interface IPackageQuery
    {
        IQueryable<PackageEntity> GetAll();
        Task<PackageEntity> GetById(Guid id);
    }
}
