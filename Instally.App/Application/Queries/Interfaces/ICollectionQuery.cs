using Instally.App.Application.Entities;

namespace Instally.App.Application.Queries.Interfaces
{
    public interface ICollectionQuery
    {
        IQueryable<CollectionEntity> GetAll();
        Task<CollectionEntity> GetById(Guid id);
    }
}
