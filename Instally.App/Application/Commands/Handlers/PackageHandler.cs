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
    public class PackageHandler : IRequestHandler<AddPackageCommand, bool>
    {

        private readonly IAppRepository<PackageEntity> _packageRepository;

        public PackageHandler(IAppRepository<PackageEntity> packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<bool> Handle(AddPackageCommand message, CancellationToken cancellationToken)
        {
            foreach (PackageEntity pkg in message.Packages)
            {
                PackageEntity package = new(pkg.WingetId, pkg.Name, pkg.Publisher, pkg.Tags, pkg.Description, pkg.Site, pkg.VersionsLength, pkg.LatestVersion, pkg.Score);

                _packageRepository.Add(package);
            }

            return await _packageRepository.UnitOfWork.Save();
        }
    }
}
