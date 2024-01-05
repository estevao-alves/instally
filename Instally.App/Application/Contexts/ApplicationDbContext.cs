using Microsoft.EntityFrameworkCore;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;

namespace Instally.App.Application.Contexts
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CollectionEntity> Collections { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ESTEVAO\\SQLEXPRESS;Initial Catalog=Instally;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(c =>
            {
                c.Property(x => x.Name).HasMaxLength(100);
                c.Property(x => x.Email).HasMaxLength(80);
            });

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Save()
        {
            try
            {
                await base.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex.InnerException;
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
