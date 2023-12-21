using FluentValidation;

namespace InstallyApp.Application.Commands.UserCommands.Validators
{
    public sealed class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Email)
            .Must(id =>
            {
                using (var db = HostContext.AppHost.GetDbConnection(base.Request))
                {
                    return !db.Exists<AddUserCommand>(x => x.Email == email);
                }
            })
            .WithErrorCode("AlreadyExists")
            .WithMessage("...");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Informe o Email.");
            RuleFor(x => x.Senha).NotEmpty().WithMessage("Informe a Senha.");
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
