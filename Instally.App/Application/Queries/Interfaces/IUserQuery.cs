using Instally.App.Application.Entities;

namespace Instally.App.Application.Queries.Interfaces
{
    public interface IUserQuery
    {
        IQueryable<UserEntity> GetAll();
        Task<UserEntity> GetById(Guid id);
    }
}
