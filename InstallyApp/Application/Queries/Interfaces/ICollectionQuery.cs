using InstallyApp.Application.Entities;

namespace InstallyApp.Application.Queries.Interfaces
{
    public interface ICollectionQuery
    {
        IQueryable<CollectionEntity> GetAll();
        Task<CollectionEntity> GetById(int id);
    }
}
