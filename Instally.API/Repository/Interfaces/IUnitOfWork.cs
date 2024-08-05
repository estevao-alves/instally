namespace Instally.API.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Save();
    }
}