using FluentValidation;
using Instally.App.Application.Commands.UserCommands;
using Instally.App.Application.Models;
using Instally.App.Application.Queries;
using Instally.App.Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Instally.App.Components.Janelas
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
            AddUserCommand command = new(TextBox_Email.Valor, TextBox_Senha.Valor);

            try
            {
                TextBox_Email.Erro = string.Empty;
                TextBox_Senha.Erro = string.Empty;
                bool resultado = await Master.Mediator.Send(command);

                if (resultado)
                {
                    Master.Usuario = new(TextBox_Email.Valor, TextBox_Senha.Valor);
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
