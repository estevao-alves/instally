﻿using MediatR;

namespace Instally.API.Commands.CollectionCommands
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
