using InstallyApp.Components.Layout;
using System.Threading;

namespace InstallyApp.Components.Popups
{
    public partial class JanelaDeInstalacaoDeApps : UserControl
    {

        public List<AppParaInstalar> ListaDeAppParaInstalar { get; set; }

        public EnumState currentState { get; set; }

        public enum EnumState
        {
            Checking,
            Waiting,
            Installing,
            Error
        }

        public JanelaDeInstalacaoDeApps()
        {
            InitializeComponent();
            DataContext = this;
            ListaDeAppParaInstalar = new();
        }

        public void InstalacaoEstado(EnumState state, string textoDetalhes="", bool inProgress=true)
        {
            currentState = state;

            switch (state.ToString())
            {
                case "Checking":
                    Titulo.Visibility = Visibility.Visible;
                    Titulo.Text = "Checking...";
                    TextoDetalhes.Text = textoDetalhes;
                    TextoDetalhes.FontSize = 14;
                    BarraDeProgresso.Visibility = Visibility.Visible;
                    Botoes.Visibility = Visibility.Collapsed;

                    break;

                case "Waiting":
                    Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Master.Main.AreaDePopups.Children.Clear();
                    CloseButton.Click += (object sender, RoutedEventArgs e) => Master.Main.AreaDePopups.Children.Clear();
                    MinimizeButton.Click += (object sender, RoutedEventArgs e) => Master.Main.AreaDePopups.Children.Clear();

                    MinimizeButton.Visibility = Visibility.Collapsed;

                    Titulo.Visibility = Visibility.Collapsed;
                    TextoDetalhes.Text = textoDetalhes;
                    TextoDetalhes.FontSize = 18;

                    Botoes.Visibility = Visibility.Visible;
                    Confirmar.Visibility = Visibility.Visible;
                    ConfirmarTextBlock.Text = "✔️";

                    BarraDeProgresso.Visibility = Visibility.Collapsed;

                    break;

                case "Installing":
                    Titulo.Text = "Installing...";

                    TextoDetalhes.FontSize = 14;
                    MinimizeButton.Visibility = Visibility.Visible;
                    Titulo.Visibility = Visibility.Visible;
                    Botoes.Visibility = Visibility.Collapsed;
                    BarraDeProgresso.Visibility = Visibility.Visible;

                    break;

                case "Error":
                    Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Master.Main.AreaDePopups.Children.Clear();

                    Titulo.Text = "Installation error";
                    BarraDeProgresso.Visibility = Visibility.Collapsed;
                    TextoDetalhes.Text = textoDetalhes;
                    Botoes.Visibility = Visibility.Visible;
                break;
            }
        }

        public async void IniciarVerificacao()
        {
            Master.Main.AreaDePopups.Children.Add(this);

            if (currentState != EnumState.Installing) InstalacaoEstado(EnumState.Checking);

            List<string> appsJaInstalados = new();

            if (ListaDeAppParaInstalar.Count > 0)
            {
                for (int i = 0; i < ListaDeAppParaInstalar.Count; i++)
                {
                    string appCodeId = ListaDeAppParaInstalar[i].CodeId;
                    string appName = ListaDeAppParaInstalar[i].Name;

                    // Verificar se o app já está instalado
                    Debug.WriteLine(ListaDeAppParaInstalar.Count);

                    TextoDetalhes.Text = $"{appName} ({i + 1}/{ListaDeAppParaInstalar.Count})";
                    string result = await Command.Executar("cmd.exe", $"/c; {Command.wingetExe} list -q {appCodeId}");

                    // Se já tiver instalado, então...
                    if (!result.Contains(appCodeId)) appsJaInstalados.Add(appCodeId);
                }

                try
                {
                    if (ListaDeAppParaInstalar.Count == appsJaInstalados.Count)
                    {
                        string textoDetalhes = "All selected apps are already installed";
                        throw new Exception(textoDetalhes);
                    }

                    if (appsJaInstalados.Count > 0) {

                        Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) =>
                        {
                            List<AppParaInstalar> appsPermitidosParaInstalar = new();

                            foreach (AppParaInstalar app in ListaDeAppParaInstalar)
                            {
                                string? exit = appsJaInstalados.Find(appStringCodeId => appStringCodeId == app.CodeId);
                                if (exit is null) appsPermitidosParaInstalar.Add(app);
                            }

                            IniciarInstalacao(appsPermitidosParaInstalar);
                        };

                        string textoDetalhes = $"{appsJaInstalados.Count} app{(appsJaInstalados.Count > 1 ? "s" : "")} is already installed \n continue the installation?";
                        throw new Exception(textoDetalhes);
                    };

                }
                catch (Exception ex) {
                    InstalacaoEstado(EnumState.Waiting, ex.Message);

                    IniciarInstalacao(ListaDeAppParaInstalar);
                }
            }
            else
            {
                string textoDetalhes = "Select at least one app to install";
                InstalacaoEstado(EnumState.Waiting, textoDetalhes);
            }
        }

        public async void IniciarInstalacao(List<AppParaInstalar> apps)
        {
            InstalacaoEstado(EnumState.Installing);

            List<string> appsJaInstalados = new();

            try
            {
                for (int i = 1; i <= apps.Count; i++)
                {
                    string appCodeId = apps[i - 1].CodeId;
                    string appName = apps[i - 1].Name;

                    InstalacaoEstado(currentState, $"{appName} ({i - 1}/{apps.Count})");

                    // Verificar se o app já está instalado
                    BarraDeProgresso.IsIndeterminate = true;
                    TextoDetalhes.Text = $"{appName} ({i - 1}/{apps.Count})";
                    string result = await Command.Executar("cmd.exe", $"/c; {Command.wingetExe} install {appCodeId}");

                    BarraDeProgresso.Value = (i * 100) / apps.Count;
                }

                for (int i = 0; i < ListaDeAppParaInstalar.Count; i++)
                {
                    string appCodeId = ListaDeAppParaInstalar[i].CodeId;
                    string appName = ListaDeAppParaInstalar[i].Name;

                    // Verificar se o app já está instalado
                    TextoDetalhes.Text = $"{appName} ({i + 1}/{ListaDeAppParaInstalar.Count})";
                    string result = await Command.Executar("cmd.exe", $"/c; {Command.wingetExe} list -q {appCodeId}");

                    // Se já tiver instalado, então...
                    if (result.Contains(appCodeId)) InstalacaoEstado(EnumState.Waiting, "All apps were successfully installed! 💫");
                    
                    // Se der algum erro na instalacao
                    else throw new Exception($"Error installing {appName}.");
                }
            }
            catch (Exception ex)
            {
                InstalacaoEstado(EnumState.Error, ex.Message);
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Master.Main.Footer.BarraDeProgresso.Visibility = Visibility.Visible;

            // Master.Main.Footer.InstallyButton.Text = textoDetalhes;
            Master.Main.AreaDePopups.Children.Clear();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Command.Executar("cmd.exe", "/c; powershell; Stop-Process -Name 'winget' -Force");

            Master.Main.Footer.BarraDeProgresso.Visibility = Visibility.Collapsed;
            Master.Main.Footer.InstallyButton.Text = "Instally";
            Master.Main.AreaDePopups.Children.Clear();
        }
    }
}
