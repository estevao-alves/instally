using MediatR;
using Instally.App.Application;
using Instally.API.Commands.CollectionCommands;
using Instally.API.Models;
using Instally.API.Repository.Interfaces;
using Instally.API.Queries.Interfaces;

namespace Instally.API.Handlers
{
    public class CollectionHandler :
                                IRequestHandler<AddCollectionCommand, bool>,
                                IRequestHandler<DeleteCollectionCommand, bool>

    {
        private readonly IAppRepository<CollectionModel> _collectionRepository;

        public CollectionHandler(IAppRepository<CollectionModel> packageRepository)
        {
            _collectionRepository = packageRepository;
        }

        public async Task<bool> Handle(AddCollectionCommand message, CancellationToken cancellationToken)
        {
            CollectionModel package = new(message.Title, message.UserId, message.Packages);

            _collectionRepository.Add(package);

            return await _collectionRepository.UnitOfWork.Save();
        }

        public async Task<bool> Handle(DeleteCollectionCommand message, CancellationToken cancellationToken)
        {
            var collectionQuery = Master.ServiceProvider.GetService<ICollectionQuery>();
            CollectionModel collection = await collectionQuery.GetById(message.CollectionId);

            _collectionRepository.Delete(collection);

            return await _collectionRepository.UnitOfWork.Save();
        }
    }
}
