
using InstallyApp.Application.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InstallyApp.Application.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        DatabaseFacade Database { get; }
    }
}
