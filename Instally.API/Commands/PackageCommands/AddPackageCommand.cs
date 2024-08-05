using Instally.API.Models;
using MediatR;

namespace Instally.API.Commands.PackageCommands
{
    public class AddPackageCommand : IRequest<bool>
    {
        public List<PackageModel> Packages { get; set; }
        public Guid? CollectionId { get; set; }

        public AddPackageCommand(List<PackageModel> packages)
        {
            Packages = packages;
        }
    }
}
