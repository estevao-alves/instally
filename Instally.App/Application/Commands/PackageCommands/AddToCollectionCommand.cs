using Instally.App.Application.Entities;
using Instally.App.Components.Items;
using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class AddToCollectionCommand : IRequest<bool>
    {
        public Guid PackageId { get; set; }
        public Guid CollectionId { get; set; }

        public AddToCollectionCommand(Guid packageId, Guid collectionId)
        {
            PackageId = packageId;
            CollectionId = collectionId;
        }
    }
}
