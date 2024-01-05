using Instally.App.Application.Queries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.App.Application.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public List<CollectionEntity>? Collections { get; set; }

        public UserEntity(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public UserEntity(string name, string email, List<CollectionEntity> collections)
        {
            Name = name;
            Email = email;
            Collections = collections;
        }
    }
}
