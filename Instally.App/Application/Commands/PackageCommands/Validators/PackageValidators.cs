using FluentValidation;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;
using System.Threading;

namespace Instally.App.Application.Commands.PackageCommands.Validators
{
    public sealed class AddPackageValidator : AbstractValidator<AddPackageCommand>
    {
        public AddPackageValidator()
        {
            RuleFor(c => c.Packages).NotEmpty().WithMessage("Informe o applicativo.");
        }
    }

    public sealed class AddToCollectionValidator : AbstractValidator<AddToCollectionCommand>
    {
        public AddToCollectionValidator()
        {
            RuleFor(c => c.CollectionId).NotEmpty().WithMessage("Informe a coleção.");
        }
    }
}
