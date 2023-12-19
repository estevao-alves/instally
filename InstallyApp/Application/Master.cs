using MediatR;

namespace InstallyApp.Application
{
    public static class Master
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IMediator Mediator { get; set; }

        public static DebugStatus Debug;
        public static MainWindow Main;
    }
}
