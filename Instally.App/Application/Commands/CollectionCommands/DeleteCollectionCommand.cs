using MediatR;

namespace Instally.App.Application.Commands.UserCommands
{
    public class DeleteCollectionCommand : IRequest<bool>
    {
        public Guid CollectionId { get; set; }

        public DeleteCollectionCommand(Guid collectionId)
        {
            CollectionId = collectionId;
        }
    }
}
