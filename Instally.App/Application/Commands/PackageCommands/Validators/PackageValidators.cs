using FluentValidation;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;
using System.Threading;

namespace Instally.App.Application.Commands.PackageCommands.Validators
{
    public sealed class AddPackageValidator : AbstractValidator<AddPackageCommand>
    {
        public AddPackageValidator(IAppRepository<PackageEntity> userRepository)
        {
            RuleFor(c => c.Packages).NotEmpty().WithMessage("Informe o applicativo.");
        }
    }
}
