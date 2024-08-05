using Instally.API.Models;
using MediatR;

namespace Instally.API.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserModel User { get; set; }

        public UpdateUserCommand(string email, string password)
        {
            Password = password;
            Email = email;
        }

        public void AssociarDados(UserModel user)
        {
            User = user;
        }
    }
}
