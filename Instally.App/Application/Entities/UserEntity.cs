using Instally.App.Application.Queries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.App.Application.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<CollectionEntity> Collections { get; set; }

        public UserEntity(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public void Atualizar(string email, string senha)
        {
            Senha = senha;
            Email = email;
        }
    }
}
