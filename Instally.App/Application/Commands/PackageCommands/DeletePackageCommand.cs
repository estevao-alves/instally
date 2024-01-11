using Instally.App.Application.Entities;
using Instally.App.Components.Items;
using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class DeletePackageCommand : IRequest<bool>
    {
        public Guid PackageId { get; set; }
        public Guid? CollectionId { get; set; }

        public DeletePackageCommand(Guid packages)
        {
            PackageId = packages;
        }
    }
}
