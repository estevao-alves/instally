using Instally.API.Models;

namespace Instally.API.Queries.Interfaces
{
    public interface IUserQuery
    {
        IQueryable<UserModel> GetAll();
        Task<UserModel> GetById(Guid id);
    }
}
