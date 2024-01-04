using InstallyApp.Application.Entities;
using InstallyApp.Application.Queries.Interfaces;
using InstallyApp.Application.Repository.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Queries
{
    public class CollectionQuery : ICollectionQuery
    {
        private readonly ApplicationDbContext _appRepository;

        public CollectionQuery(ApplicationDbContext appRepository)
        {
            _appRepository = appRepository;
        }

        public IQueryable<CollectionEntity> GetAll()
        {
            return _appRepository.Collections.AsQueryable();
        }

        public Task<CollectionEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
