using InstallyApp.Application.Entities;
using InstallyApp.Components.Items;
using MediatR;

namespace InstallyApp.Application.Commands.UserCommands
{
    public class AddPackageCommand : IRequest<bool>
    {
        public List<PackageEntity> Packages { get; set; }
        public string? Collection { get; set; }
        public int? CollectionId { get; set; }

        public AddPackageCommand(List<PackageEntity> packages)
        {
            Packages = packages;
        }
    }
}
