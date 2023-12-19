namespace InstallyApp.Application.Queries.Interfaces
{
    public interface IAppQuery
    {
        Task<IEnumerable<AppQuery>> GetAll();
        Task<AppQuery> GetById(int id);
    }
}
