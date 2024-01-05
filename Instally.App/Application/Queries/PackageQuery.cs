using Instally.App.Application.Entities;
using Instally.App.Application.Queries.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Instally.App.Application.Queries
{
    public class PackageQuery : IPackageQuery
    {
        private readonly ApplicationDbContext _appRepository;

        public PackageQuery(ApplicationDbContext appRepository)
        {
            _appRepository = appRepository;
        }
        public IQueryable<PackageEntity> GetAll()
        {
            return _appRepository.Packages.AsQueryable();
        }

        public Task<PackageEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
