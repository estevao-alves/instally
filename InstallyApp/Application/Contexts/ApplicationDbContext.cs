using Microsoft.EntityFrameworkCore;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;

namespace InstallyApp.Application.Contexts
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

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
                c.HasKey(x => x.Id);
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
