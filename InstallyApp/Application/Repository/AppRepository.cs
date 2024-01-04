using Microsoft.EntityFrameworkCore.Infrastructure;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InstallyApp.Application.Repository
{
    public class AppRepository<T> : IDisposable, IAppRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public DatabaseFacade Database => _context.Database;

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetById(int id)
        {
            return _context.Set<T>().Where(x => x.Id == id);
        }

        public void Add(T item)
        {
            _context.Add(item);
        }

        public void Update(T item)
        {
            _context.Update(item);
        }

        public void Delete(T item)
        {
            _context.Remove(item);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<bool> IsUniqueUserAsync(string email)
        {
            return !await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
