using Instally.App.Models;

namespace Instally.App.DataServices
{
    public interface IRestDataService
    {
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User toDo);
        Task UpdateUserAsync(User toDo);
        Task DeleteUserAsync(Guid id);
    }
}
