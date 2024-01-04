using FluentValidation;
using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;
using System.Threading;

namespace InstallyApp.Application.Commands.PackageCommands.Validators
{
    public sealed class AddPackageValidator : AbstractValidator<AddPackageCommand>
    {
        public AddPackageValidator(IAppRepository<PackageEntity> userRepository)
        {
            RuleFor(c => c.Packages).NotEmpty().WithMessage("Informe o applicativo.");
        }
    }
}
