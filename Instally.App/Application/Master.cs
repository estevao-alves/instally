using Instally.App.Application.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Instally.App.Application
{
    public static class Master
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IMediator Mediator { get; set; }
        public static IConfiguration Config { get; set; }
        public static UserEntity UsuarioAutenticado { get; set; }
        public static AppSettings AppSettings { get; set; }
        public static List<PackageEntity> Packages { get; set; }
        public static List<CollectionEntity> Collections { get; set; }

        public static DebugStatus Debug;
        public static MainWindow Main;
    }
}
