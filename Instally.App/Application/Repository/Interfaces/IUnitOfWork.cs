namespace Instally.App.Application.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Save();
    }
}