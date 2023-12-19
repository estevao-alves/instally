using InstallyApp.Application.Queries;

namespace InstallyApp.Application.Queries.Interfaces
{
    public interface ICollectionQuery
    {
        Task<IEnumerable<CollectionQuery>> GetAll();
        Task<CollectionQuery> GetById(int id);

    }
}
