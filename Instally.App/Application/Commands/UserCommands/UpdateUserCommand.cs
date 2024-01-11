using Instally.App.Application.Entities;
using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public UserEntity Usuario { get; set; }

        public UpdateUserCommand(string email, string senha)
        {
            Senha = senha;
            Email = email;
        }

        public void AssociarDados(UserEntity usuario)
        {
            Usuario = usuario;
        }
    }
}
