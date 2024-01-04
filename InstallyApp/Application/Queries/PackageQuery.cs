using InstallyApp.Application.Entities;
using InstallyApp.Application.Queries.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InstallyApp.Application.Queries
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

        public Task<PackageEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
