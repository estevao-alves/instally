using MediatR;
using System.Threading;
using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;
using InstallyApp.Application.Repository;
using FluentValidation;
using InstallyApp.Components.Items;

namespace InstallyApp.Application.Commands.Handlers.Package
{
    public class PackageHandler : IRequestHandler<AddPackageCommand, bool>
    {

        private readonly IAppRepository<PackageEntity> _userRepository;

        public PackageHandler(IAppRepository<PackageEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddPackageCommand message, CancellationToken cancellationToken)
        {
            foreach (PackageEntity pkg in message.Packages)
            {
                PackageEntity package = new(pkg.WingetId, pkg.Name, pkg.Publisher, pkg.Description, pkg.Site, pkg.VersionsLength, pkg.LatestVersion, pkg.Score);

                _userRepository.Add(package);
            }

            return await _userRepository.UnitOfWork.Save();
        }
    }
}
