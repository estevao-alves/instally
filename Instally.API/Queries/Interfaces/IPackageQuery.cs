using Instally.API.Models;

namespace Instally.API.Queries.Interfaces
{
    public interface IPackageQuery
    {
        IQueryable<PackageModel> GetAll();
        Task<PackageModel> GetById(Guid id);
    }
}
