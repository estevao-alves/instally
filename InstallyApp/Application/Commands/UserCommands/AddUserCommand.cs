using MediatR;

namespace InstallyApp.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Collection { get; set; }
        public int? CollectionId { get; set; }

        public AddUserCommand(string name, string email)
        {
            Name = name;
            Email = email;

            Collection = null;
            CollectionId = null;
        }
    }
}
