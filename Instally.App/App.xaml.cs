using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Instally.App.Application.Repository;
using Instally.App.Application.Repository.Interfaces;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Commands.UserCommands.Behaviors;
using Instally.App.Application.Commands.UserCommands.Validators;
using Instally.App.Application.Queries.Interfaces;
using Instally.App.Application.Queries;
using Instally.App.Application.Commands.PackageCommands.Validators;
using System.IO;

namespace Instally.App
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            Master.ServiceProvider = ConfigureServices().BuildServiceProvider();
            Master.Mediator = Master.ServiceProvider.GetRequiredService<IMediator>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WingetData.CarregarPacotesDaAPI();

            Master.Main = new();
            Master.Main.Show();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindow>();
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));

            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<App>());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<ICollectionQuery, CollectionQuery>();
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IPackageQuery, PackageQuery>();

            // UserCommands
            services.AddTransient<IValidator<AddUserCommand>, AddUserValidator>();
            services.AddTransient<IValidator<UpdateUserCommand>, UpdateUserValidator>();
            services.AddTransient<IValidator<DeleteUserCommand>, DeleteUserValidator>();

            // PackageCommands
            services.AddTransient<IValidator<AddPackageCommand>, AddPackageValidator>();

            return services;
        }
    }
}
