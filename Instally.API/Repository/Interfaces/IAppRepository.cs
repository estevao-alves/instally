using Instally.API.Models;

namespace Instally.API.Repository.Interfaces
{
    public interface IAppRepository<T> : IRepository<T> where T : BaseModel
    {
        Task<bool> IsUniqueUserAsync(string email);
        void Add(T app);
        void Update(T app);
        void Delete(T app);
    }
}
