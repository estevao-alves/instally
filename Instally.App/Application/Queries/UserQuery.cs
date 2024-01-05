using Instally.App.Application.Entities;
using Instally.App.Application.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.App.Application.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly ApplicationDbContext _appRepository;

        public UserQuery(ApplicationDbContext appRepository)
        {
            _appRepository = appRepository;
        }

        public Task<IEnumerable<UserEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetById(Guid id)
        {
            return await _appRepository.Set<UserEntity>().FindAsync(id);
        }
    }
}
