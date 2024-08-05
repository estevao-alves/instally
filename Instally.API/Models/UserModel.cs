namespace Instally.API.Models
{
    public class UserModel : BaseModel
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<CollectionModel> Collections { get; set; }

        public UserModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void Atualizar(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
