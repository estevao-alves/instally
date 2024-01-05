using MediatR;
using System.Threading;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;
using Instally.App.Application.Repository;
using FluentValidation;
using Instally.App.Components.Items;

namespace Instally.App.Application.Commands.Handlers
{
    public class CollectionHandler : IRequestHandler<AddCollectionCommand, bool>
    {

        private readonly IAppRepository<CollectionEntity> _collectionRepository;

        public CollectionHandler(IAppRepository<CollectionEntity> packageRepository)
        {
            _collectionRepository = packageRepository;
        }

        public async Task<bool> Handle(AddCollectionCommand message, CancellationToken cancellationToken)
        {
            CollectionEntity package = new(message.Title, message.UserId, message.Packages);

            _collectionRepository.Add(package);

            return await _collectionRepository.UnitOfWork.Save();
        }
    }
}
