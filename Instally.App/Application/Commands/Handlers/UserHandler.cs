using MediatR;
using System.Threading;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Entities;
using Instally.App.Application.Repository.Interfaces;
using System.Data;
using Instally.App.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
            UserEntity user = new(message.Email, message.Senha);

            _userRepository.Add(user);

            return await _userRepository.UnitOfWork.Save();
        }

        public async Task<bool> Handle(UpdateUserCommand message, CancellationToken cancellationToken)
        {
            var userQuery = Master.ServiceProvider.GetService<IUserQuery>();
            UserEntity user = userQuery.GetAll().FirstOrDefault();

            user.Atualizar(message.Email, message.Senha);

            _userRepository.Update(user);

            message.AssociarDados(user);

            var resultado = await _userRepository.UnitOfWork.Save();

            return resultado;
        }

        public async Task<bool> Handle(DeleteUserCommand message, CancellationToken cancellationToken)
        {
            UserEntity user = Master.ServiceProvider.GetService<IUserQuery>().GetAll().Where(w => w.Email == message.Email).FirstOrDefault();

            _userRepository.Delete(user);

            var resultado = await _userRepository.UnitOfWork.Save();

            return resultado;
        }
    }
}
