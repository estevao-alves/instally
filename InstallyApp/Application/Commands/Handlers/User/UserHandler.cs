using MediatR;
using System.Threading;
using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Entities;
using InstallyApp.Application.Repository.Interfaces;

namespace InstallyApp.Application.Commands.Handlers.User
{
    public class UserHandler : IRequestHandler<AddUserCommand, bool>
    {

        private readonly IAppRepository<UserEntity> _userRepository;

        public UserHandler(IAppRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddUserCommand message, CancellationToken cancellationToken)
        {
            UserEntity user = new(message.Name, message.Email);

            _userRepository.Add(user);

            return await _userRepository.UnitOfWork.Save();
        }

    }
}
