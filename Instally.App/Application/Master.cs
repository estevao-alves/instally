using Instally.App.Application.Models;
using MediatR;

namespace Instally.App.Application
{
    public static class Master
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IMediator Mediator { get; set; }
        public static UsuarioAutenticado Usuario { get; set; }

        public static DebugStatus Debug;
        public static MainWindow Main;
    }
}
