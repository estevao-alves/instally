using Instally.App.Application.Entities;
using Instally.App.Application.Queries.Interfaces;
using Instally.App.Application.Repository.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.App.Application.Queries
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

        public Task<CollectionEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
