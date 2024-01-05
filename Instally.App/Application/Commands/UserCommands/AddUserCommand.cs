using Instally.App.Application.Entities;
using Instally.App.Components.Items;
using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public AddUserCommand(string email, string senha)
        {
            Name = email.Split("@")[0];
            Senha = senha;
            Email = email;
        }
    }
}
