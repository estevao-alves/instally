using MediatR;
using Microsoft.Extensions.DependencyInjection;
using InstallyApp.Application.Repository;
using InstallyApp.Application.Repository.Interfaces;
using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Commands.Handlers.User;

namespace InstallyApp
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            Master.ServiceProvider = ConfigureServices().BuildServiceProvider();
            Master.Mediator = Master.ServiceProvider.GetRequiredService<IMediator>();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await WingetData.CarregarPacotesDaAPI();

            Master.Main = new();
            Master.Main.Show();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindow>();

            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));

            services.AddMediatR(typeof(App));

            // UserCommands
            services.AddScoped<IRequestHandler<AddUserCommand, bool>, UserHandler>();

            return services;
        }
    }
}
