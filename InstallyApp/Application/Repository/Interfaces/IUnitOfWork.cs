namespace InstallyApp.Application.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Save();
    }
}
