using InstallyApp.Application.Queries;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstallyApp.Application.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public UserEntity(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
