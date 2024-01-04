
using InstallyApp.Application.Entities;

namespace InstallyApp.Application.Repository.Interfaces
{
    public interface IAppRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> IsUniqueUserAsync(string email);
        void Add(T app);
        void Update(T app);
        void Delete(T app);
    }
}
