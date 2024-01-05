using Instally.App.Application.Entities;
using Instally.App.Components.Items;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instally.App.Application.Commands.UserCommands
{
    public class AddCollectionCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public List<PackageEntity>? Packages { get; set; }

        public AddCollectionCommand(string title, Guid userId, List<PackageEntity>? packages)
        {
            UserId = userId;
            Title = title;
            Packages = packages;
        }
    }
}
