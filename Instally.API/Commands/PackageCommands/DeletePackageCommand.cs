using MediatR;

namespace Instally.API.Commands.PackageCommands
{
    public class DeletePackageCommand : IRequest<bool>
    {
        public Guid PackageId { get; set; }
        public Guid? CollectionId { get; set; }

        public DeletePackageCommand(Guid packages)
        {
            PackageId = packages;
        }
    }
}
