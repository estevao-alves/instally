using MediatR;
using System.Threading;
using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;
using InstallyApp.Application.Repository;
using FluentValidation;

namespace InstallyApp.Application.Commands.Handlers.User
{
    public class UserHandler : IRequestHandler<AddUserCommand, bool>, IRequestHandler<UpdateUserCommand, bool>, IRequestHandler<DeleteUserCommand, bool>
    {

        private readonly IAppRepository<UserEntity> _userRepository;

        public UserHandler(IAppRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddUserCommand message, CancellationToken cancellationToken)
        {
            UserEntity user = new(message.Senha, message.Email);

            _userRepository.Add(user);

            return await _userRepository.UnitOfWork.Save();
        }

        public async Task<bool> Handle(UpdateUserCommand message, CancellationToken cancellationToken)
        {
            UserEntity user = new(message.Senha, message.Email);

            _userRepository.Update(user);

            return await _userRepository.UnitOfWork.Save();
        }

        public async Task<bool> Handle(DeleteUserCommand message, CancellationToken cancellationToken)
        {
            UserEntity user = new(message.Senha, message.Email);

            _userRepository.Delete(user);

            return await _userRepository.UnitOfWork.Save();
        }
    }
}
