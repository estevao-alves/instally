using InstallyApp.Components.Items;
using MediatR;

namespace InstallyApp.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Collection { get; set; }
        public int? CollectionId { get; set; }

        public AddUserCommand(string email, string senha)
        {
            Senha = senha;
            Email = email;

            Name = null;
            Collection = null;
            CollectionId = null;
        }
    }
}
