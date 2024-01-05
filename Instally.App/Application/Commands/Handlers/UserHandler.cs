using MediatR;
using System.Threading;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;

namespace Instally.App.Application.Commands.Handlers
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
