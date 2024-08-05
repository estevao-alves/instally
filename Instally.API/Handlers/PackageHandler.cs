using MediatR;
using Instally.App.Application;
using Instally.API.Commands.PackageCommands;
using Instally.API.Models;
using Instally.API.Repository.Interfaces;
using Instally.API.Queries.Interfaces;

namespace Instally.API.Handlers
{
    public class PackageHandler : IRequestHandler<AddPackageCommand, bool>, IRequestHandler<AddToCollectionCommand, bool>
    {

        private readonly IAppRepository<PackageModel> _packageRepository;

        public PackageHandler(IAppRepository<PackageModel> packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<bool> Handle(AddPackageCommand message, CancellationToken cancellationToken)
        {
            foreach (PackageModel pkg in message.Packages)
            {
                PackageModel package = new(pkg.WingetId, pkg.Name, pkg.Publisher, pkg.Tags, pkg.Description, pkg.Site, pkg.VersionsLength, pkg.LatestVersion, pkg.Score);

                _packageRepository.Add(package);
            }

            return await _packageRepository.UnitOfWork.Save();
        }

        public async Task<bool> Handle(AddToCollectionCommand message, CancellationToken cancellationToken)
        {
            PackageModel Package = await Master.ServiceProvider.GetService<IPackageQuery>().GetById(message.PackageId);
            CollectionModel Collection = await Master.ServiceProvider.GetService<ICollectionQuery>().GetById(message.CollectionId);

            // Package.AtualizarCollection(message.CollectionId, Collection);

            _packageRepository.Update(Package);

            return await _packageRepository.UnitOfWork.Save();
        }
    }
}
