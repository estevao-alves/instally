using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public UpdateUserCommand(string? name, string email, string senha)
        {
            Name = name;
            Senha = senha;
            Email = email;
        }
    }
}
