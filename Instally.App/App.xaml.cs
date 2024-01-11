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
using Instally.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Instally.App
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            Master.AppSettings = CarregarAppSettings();
            Master.ServiceProvider = ConfigureServices().BuildServiceProvider();
            Master.Mediator = Master.ServiceProvider.GetRequiredService<IMediator>();
            
            ConfigureMySql();
        }

        public AppSettings CarregarAppSettings()
        {
            if (!File.Exists(AppSettings.CaminhoDoArquivo))
            {
                AppSettings preferencias = new();

                File.WriteAllText(AppSettings.CaminhoDoArquivo, FuncoesJson.ClasseParaJson(preferencias));

                return preferencias;
            }
            else return FuncoesJson.JsonParaClasse<AppSettings>(AppSettings.CaminhoDoArquivo);
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            WingetData.CarregarPacotesDaAPI();

            Master.Main = new();
            Master.Main.Show();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(AppSettings.ConnectionString, ServerVersion.AutoDetect(AppSettings.ConnectionString)));

            services.AddTransient<MainWindow>();
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<ICollectionQuery, CollectionQuery>();
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IPackageQuery, PackageQuery>();
            services.AddSingleton<DataAcess>();

            // UserCommands
            services.AddScoped<IValidator<AddUserCommand>, AddUserValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserValidator>();
            services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserValidator>();

            // PackageCommands
            services.AddScoped<IValidator<AddPackageCommand>, AddPackageValidator>();
            services.AddScoped<IValidator<AddToCollectionCommand>, AddToCollectionValidator>();

            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<App>());

            return services;
        }

        private void ConfigureMySql()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Master.Config = configuration;

            using (var connection = new MySqlConnection(AppSettings.ConnectionString))
            {
                connection.Open();
            }
        }
    }
}
