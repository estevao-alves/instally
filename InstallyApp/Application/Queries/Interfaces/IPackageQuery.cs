using InstallyApp.Application.Entities;

namespace InstallyApp.Application.Queries.Interfaces
{
    public interface IPackageQuery
    {
        IQueryable<PackageEntity> GetAll();
        Task<PackageEntity> GetById(int id);
    }
}
