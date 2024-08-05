using Instally.API.Models;
using MediatR;

namespace Instally.API.Commands.CollectionCommands
{
    public class AddCollectionCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public List<PackageModel>? Packages { get; set; }

        public AddCollectionCommand(string title, Guid userId, List<PackageModel>? packages)
        {
            UserId = userId;
            Title = title;
            Packages = packages;
        }
    }
}
