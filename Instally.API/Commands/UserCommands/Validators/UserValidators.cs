using FluentValidation;
using Instally.API.Models;
using Instally.API.Repository.Interfaces;

namespace Instally.API.Commands.UserCommands.Validators
{
    public sealed class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator(IAppRepository<UserModel> userRepository)
        {
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Email invalid.")
                .NotEmpty().WithMessage("Enter your Email.")
                .MustAsync(async (email, _) =>
                {
                    return await userRepository.IsUniqueUserAsync(email);
                }).WithMessage("There is already a user with this email address");

            RuleFor(c => c.Password).NotEmpty().WithMessage("Enter your Password.");
        }
    }

    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter your Email.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter your Password.");
        }
    }
    public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter your Email.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter your Password.");
        }
    }
}
