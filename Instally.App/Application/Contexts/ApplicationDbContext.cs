using Microsoft.EntityFrameworkCore;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

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
                string connectionString = AppSettings.ConnectionString;
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(c =>
            {
                c.Property(x => x.Email).HasMaxLength(100);
                c.Property(x => x.Senha).HasMaxLength(80);
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
