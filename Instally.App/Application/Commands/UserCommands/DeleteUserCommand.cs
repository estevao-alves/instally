using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public DeleteUserCommand(string email, string senha)
        {
            Senha = senha;
            Email = email;
        }
    }
}
