using Instally.API.Models;

namespace Instally.API.Queries.Interfaces
{
    public interface ICollectionQuery
    {
        IQueryable<CollectionModel> GetAll();
        Task<CollectionModel> GetById(Guid id);
    }
}
