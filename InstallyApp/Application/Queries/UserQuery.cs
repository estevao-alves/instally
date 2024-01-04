using InstallyApp.Application.Entities;
using InstallyApp.Application.Queries.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Queries
{
    public class UserQuery : IUserQuery
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("Collection")]
        public int? CollectionId { get; set; }
        public CollectionQuery? Collection { get; set; }

        public Task<IEnumerable<UserEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
