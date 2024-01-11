using FluentValidation;
using Instally.App.Application.Commands.UserCommands;

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
            try
            {
                TextBox_Email.Erro = string.Empty;
                TextBox_Senha.Erro = string.Empty;

                bool resultado;

                if (Master.UsuarioAutenticado is null)
                { 
                    AddUserCommand addCommand = new(TextBox_Email.Valor, TextBox_Senha.Valor);
                    resultado = await Master.Mediator.Send(addCommand);
                }
                else
                {
                    UpdateUserCommand updateCommand = new(TextBox_Email.Valor, TextBox_Senha.Valor);
                    resultado = await Master.Mediator.Send(updateCommand);
                }

                if (resultado)
                {
                    Master.UsuarioAutenticado = new(TextBox_Email.Valor, TextBox_Senha.Valor);
                    Master.Main.CarregarCollections();
                    Master.Main.Janelas.Children.Clear();
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
