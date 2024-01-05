namespace Instally.App.Components.Inputs
{
    public partial class CampoDeTexto : UserControl
    {
        public enum EnumInputTipos
        {
            Texto,
            ApenasNumeros,
            Email,
            Senha
        }

        string titulo;
        public string Titulo
        {
            get => titulo;
            set
            {
                TxtTitulo.Text = value;
                TxtTitulo.Visibility = value is not null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        EnumInputTipos tipo;
        public EnumInputTipos Tipo
        {
            get => tipo;
            set
            {
                tipo = value;
                switch (value)
                {
                    case EnumInputTipos.Email:
                        break;

                    case EnumInputTipos.Senha:
                        TxtValor.Visibility = Visibility.Collapsed;
                        TxtSenha.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        string valor;
        public string Valor
        {
            get => valor;
            set
            {
                valor = value;

                if (Tipo is not EnumInputTipos.Senha) TxtValor.Text = value;
                else TxtSenha.Password = value;
            }
        }

        string? erro;
        public string? Erro
        {
            get => erro;
            set
            {
                bool existe = value?.Length > 0;
                erro = value;
                TextBlock_Erro.Text = existe ? value : string.Empty;

                Border_Erro.Visibility = existe ? Visibility.Visible : Visibility.Collapsed;
                TextBlock_Erro.Visibility = existe ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public CampoDeTexto()
        {
            InitializeComponent();
            DataContext = this;
            Background = null;

            TxtSenha.Visibility = Visibility.Collapsed;
            TxtTitulo.Visibility = Visibility.Collapsed;

            Limpar();
        }

        public void Limpar()
        {
            TxtSenha.Password = string.Empty;
            TxtValor.Text = string.Empty;
            Valor = string.Empty;
        }

        private void TxtValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Erro is not null) Erro = null;
        }
        private void TxtSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Erro is not null) Erro = null;

            PasswordBox passwordBox = (PasswordBox)sender;
            valor = passwordBox.Password;
        }
    }
}
