using InstallyApp.Application.Entities;

namespace InstallyApp.Application.Queries.Interfaces
{
    public interface IUserQuery
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> GetById(int id);
    }
}
