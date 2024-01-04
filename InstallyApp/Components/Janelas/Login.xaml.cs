using FluentValidation;
using InstallyApp.Application.Commands.UserCommands;
using InstallyApp.Application.Queries;
using InstallyApp.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InstallyApp.Components.Janelas
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Master.Main.Janelas.Children.Clear();
        }

        public void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            CreateDefaultUser();
        }

        public async void CreateDefaultUser()
        {
            var command = new AddUserCommand(TextBox_Email.Valor, TextBox_Senha.Valor);

            try
            {
                TextBox_Email.Erro = string.Empty;
                TextBox_Senha.Erro = string.Empty;
                bool resultado = await Master.Mediator.Send(command);

                if (resultado)
                {
                    Master.Main.CarregarCollections();
                }
            }
            catch (ValidationException ex)
            {
                foreach (var message in ex.Errors)
                {
                    switch (message.PropertyName)
                    {
                        case "Email":
                            TextBox_Email.Erro = message.ErrorMessage;
                            break;

                        case "Senha":
                            TextBox_Senha.Erro = message.ErrorMessage;
                            break;
                    };
                }
            }
        }
    }
}
