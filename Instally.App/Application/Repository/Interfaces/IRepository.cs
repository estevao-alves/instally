
using Instally.App.Application.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Instally.App.Application.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        DatabaseFacade Database { get; }
    }
}
