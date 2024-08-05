using Instally.API.Commands.UserCommands;
using Instally.API.Models;
using Instally.API.Queries.Interfaces;
using Instally.API.Repository.Interfaces;
using Instally.App.Application;
using MediatR;
using System.Data;

namespace Instally.API.Handlers
{
    public class UserHandler : IRequestHandler<AddUserCommand, bool>, IRequestHandler<UpdateUserCommand, bool>, IRequestHandler<DeleteUserCommand, bool>
    {

        private readonly IAppRepository<UserModel> _userRepository;

        public UserHandler(IAppRepository<UserModel> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddUserCommand message, CancellationToken cancellationToken)
        {
            UserModel user = new(message.Email, message.Password);

            _userRepository.Add(user);

            return await _userRepository.UnitOfWork.Save();
        }

        public async Task<bool> Handle(UpdateUserCommand message, CancellationToken cancellationToken)
        {
            var userQuery = Master.ServiceProvider.GetService<IUserQuery>();
            UserModel user = userQuery.GetAll().FirstOrDefault();

            user.Atualizar(message.Email, message.Password);

            _userRepository.Update(user);

            message.AssociarDados(user);

            var resultado = await _userRepository.UnitOfWork.Save();

            return resultado;
        }

        public async Task<bool> Handle(DeleteUserCommand message, CancellationToken cancellationToken)
        {
            UserModel user = Master.ServiceProvider.GetService<IUserQuery>().GetAll().Where(w => w.Email == message.Email).FirstOrDefault();

            _userRepository.Delete(user);

            var resultado = await _userRepository.UnitOfWork.Save();

            return resultado;
        }
    }
}
