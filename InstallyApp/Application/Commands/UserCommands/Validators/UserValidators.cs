using FluentValidation;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;
using System.Threading;

namespace InstallyApp.Application.Commands.UserCommands.Validators
{
    public sealed class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator(IAppRepository<UserEntity> userRepository)
        {
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Email Inválido.")
                .NotEmpty().WithMessage("Informe o Email.")
                .MustAsync(async (email, _) =>
                {
                    return await userRepository.IsUniqueUserAsync(email);
                }).WithMessage("Já existe um usuario com este Email.");

            RuleFor(c => c.Senha).NotEmpty().WithMessage("Informe a Senha.");
        }
    }

    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Informe o Email.");
            RuleFor(x => x.Senha).NotEmpty().WithMessage("Informe a Senha.");
        }
    }
    public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Informe o Email.");
            RuleFor(x => x.Senha).NotEmpty().WithMessage("Informe a Senha.");
        }
    }
}
