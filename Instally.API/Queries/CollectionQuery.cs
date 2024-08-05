using Instally.API.Data;
using Instally.API.Models;
using Instally.API.Queries.Interfaces;

namespace Instally.API.Queries
{
    public class CollectionQuery : ICollectionQuery
    {
        private readonly ApplicationDbContext _appRepository;

        public CollectionQuery(ApplicationDbContext appRepository)
        {
            _appRepository = appRepository;
        }

        public IQueryable<CollectionModel> GetAll()
        {
            return _appRepository.Collections.AsQueryable();
        }

        public async Task<CollectionModel> GetById(Guid id)
        {
            return await _appRepository.Set<CollectionModel>().FindAsync(id);
        }
    }
}
