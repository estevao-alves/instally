using Instally.API.Data;
using Instally.API.Models;
using Instally.API.Queries.Interfaces;

namespace Instally.API.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly ApplicationDbContext _appRepository;

        public UserQuery(ApplicationDbContext appRepository)
        {
            _appRepository = appRepository;
        }

        public IQueryable<UserModel> GetAll()
        {
            return _appRepository.Users.AsQueryable();
        }

        public async Task<UserModel> GetById(Guid id)
        {
            return await _appRepository.Set<UserModel>().FindAsync(id);
        }
    }
}
