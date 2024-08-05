using Instally.API.Data;
using Instally.API.Models;
using Instally.API.Queries.Interfaces;

namespace Instally.API.Queries
{
    public class PackageQuery : IPackageQuery
    {
        private readonly ApplicationDbContext _appRepository;

        public PackageQuery(ApplicationDbContext appRepository)
        {
            _appRepository = appRepository;
        }
        public IQueryable<PackageModel> GetAll()
        {
            return _appRepository.Packages.AsQueryable();
        }

        public async Task<PackageModel> GetById(Guid id)
        {
            return await _appRepository.Set<PackageModel>().FindAsync(id);
        }
    }
}
