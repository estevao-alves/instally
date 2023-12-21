using FluentValidation;
using InstallyApp.Application.Commands.UserCommands;

namespace InstallyApp.Components.Popups
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
            Master.Main.AreaDePopups.Children.Clear();
        }

        public void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            CreateDefaultUser();
        }

        public async void CreateDefaultUser()
        {
            var command = new UpdateUserCommand(TextBox_Email.Valor, TextBox_Senha.Valor);

            try
            {
                TextBox_Email.Erro = string.Empty;
                TextBox_Senha.Erro = string.Empty;
                await Master.Mediator.Send(command);
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
