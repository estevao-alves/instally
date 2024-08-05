using Instally.API.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Instally.API.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseModel
    {
        IUnitOfWork UnitOfWork { get; }
        DatabaseFacade Database { get; }
    }
}
