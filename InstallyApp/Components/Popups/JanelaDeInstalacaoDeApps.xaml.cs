using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using InstallyApp.Application.Functions;
using InstallyApp.Components.Layout;
using System.Diagnostics;

namespace InstallyApp.Components.Popups
{
    public partial class JanelaDeInstalacaoDeApps : UserControl
    {

        public List<AppParaInstalar> ListaDeAppParaInstalar { get; set; }

        public List<string> AppsJaInstalados { get; set; }

        public enum EnumState
        {
            Checking,
            Confirmation,
            Installing,
            Error
        }

        public JanelaDeInstalacaoDeApps()
        {
            InitializeComponent();
            DataContext = this;
            ListaDeAppParaInstalar = new();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            App.Master.Main.Footer.BarraDeProgresso.Visibility = Visibility.Visible;
            App.Master.Main.Footer.InstallyButton.Visibility = Visibility.Collapsed;
            App.Master.Main.AreaDePopups.Children.Clear();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            InstallationState(EnumState.Confirmation, "Cancel installation?", false);
            ConfirmarTextBlock.Text = "Sim";
            Botoes.Visibility = Visibility.Visible;
            ConfirmarTextBlock.Visibility = Visibility.Visible;
            NegarTextBlock.Visibility = Visibility.Visible;
            */

            Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                Command.Executar("cmd.exe", "/c; powershell; Stop-Process -Name 'winget' -Force");

                App.Master.Main.Footer.BarraDeProgresso.Visibility = Visibility.Collapsed;
                App.Master.Main.Footer.InstallyButton.Visibility = Visibility.Visible;
                App.Master.Main.AreaDePopups.Children.Clear();
            };
        }

        public void InstallationState(EnumState state, string textDetalhes="", bool inProgress=true)
        {
            switch (state.ToString())
            {
                case "Checking":

                    Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) =>
                    {
                        List<AppParaInstalar> appsPermitidosParaInstalar = new();

                        foreach (AppParaInstalar app in ListaDeAppParaInstalar)
                        {
                            string? exit = AppsJaInstalados.Find(appStringCodeId => appStringCodeId == app.CodeId);
                            if (exit is null) appsPermitidosParaInstalar.Add(app);
                        }

                        IniciarInstalacao(appsPermitidosParaInstalar);
                    };

                    TextoDetalhes.Text = textDetalhes;
                    TextoDetalhes.FontSize = 20;
                    BarraDeProgresso.Visibility = Visibility.Collapsed;
                    Botoes.Visibility = Visibility.Visible;

                    break;

                case "Confirmation":

                    Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Visibility = Visibility.Collapsed;

                    Titulo.Visibility = Visibility.Collapsed;
                    TextoDetalhes.Text = textDetalhes;
                    TextoDetalhes.FontSize = 16;
                    Botoes.Visibility = Visibility.Visible;
                    ConfirmarTextBlock.Text = "✔️";

                    if (!inProgress)
                    {
                        BarraDeProgresso.Visibility = Visibility.Collapsed;
                        TextoDetalhes.FontSize = 18;
                        TextoDetalhes.Width = 300;
                    }

                    break;

                case "Installing":

                    Titulo.Text = "Installing...";

                    Titulo.Visibility = Visibility.Visible;
                    Botoes.Visibility = Visibility.Collapsed;
                    BarraDeProgresso.Visibility = Visibility.Visible;
                    BarraDeProgresso.IsIndeterminate = false;
                    BarraDeProgresso.Value = 0;

                    if (!inProgress)
                    {
                        Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Visibility = Visibility.Collapsed;
                        
                        Titulo.Text = "All apps successfully installed!";
                        TextoDetalhes.Visibility = Visibility.Collapsed;
                        Botoes.Visibility = Visibility.Visible;
                        ConfirmarTextBlock.Text = "✔️";
                    }

                    break;

                case "Error":
                    Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => App.Master.Main.AreaDePopups.Children.Clear();
                    {
                        // Continuar a instalação ?
                        Botoes.Visibility = Visibility.Collapsed;
                        Titulo.Text = "Installing...";
                        BarraDeProgresso.Visibility = Visibility.Visible;
                        TextoDetalhes.Text = "Installing...";
                    };

                    Titulo.Text = "Installation error";
                    BarraDeProgresso.Visibility = Visibility.Collapsed;
                    TextoDetalhes.Text = textDetalhes;
                    Botoes.Visibility = Visibility.Visible;
                break;
            }
        }

        public async void IniciarVerificacao()
        {
            List<string> appsJaInstalados = new();
            App.Master.Main.AreaDePopups.Children.Add(this);

            if (ListaDeAppParaInstalar.Count > 0)
            {
                try
                {
                    for (int i = 0; i < ListaDeAppParaInstalar.Count; i++)
                    {
                        string appCodeId = ListaDeAppParaInstalar[i].CodeId;
                        string appName = ListaDeAppParaInstalar[i].Name;

                        // Verificar se o app já está instalado
                        TextoDetalhes.Text = $"{appName} ({i + 1}/{ListaDeAppParaInstalar.Count})";
                        string result = await Command.Executar("cmd.exe", $"/c; {Command.wingetExe} list -q {appCodeId}");

                        // Se já tiver instalado, então...
                        if (result.Contains(appCodeId)) appsJaInstalados.Add(appCodeId);
                    }

                    if (ListaDeAppParaInstalar.Count == appsJaInstalados.Count)
                    {
                        string textoDetalhes = "All selected apps are already installed and updated";
                        InstallationState(EnumState.Confirmation, textoDetalhes);
                   
                        throw new Exception(textoDetalhes);
                    }

                    if (appsJaInstalados.Count > 0) {

                        AppsJaInstalados = appsJaInstalados;

                        string textoDetalhes = $"{appsJaInstalados.Count} app{(appsJaInstalados.Count > 1 ? "s" : "")} is already installed \n continue the installation?";
                        InstallationState(EnumState.Confirmation, textoDetalhes);

                        throw new Exception(textoDetalhes);
                    };

                    IniciarInstalacao(ListaDeAppParaInstalar);
                }
                catch (Exception ex)
                {
                    InstallationState(EnumState.Checking, ex.Message);
                }
            }
            else
            {
                string textoDetalhes = "Select at least one app to install";

                InstallationState(EnumState.Confirmation, textoDetalhes, false);
            }
        }

        public async void IniciarInstalacao(List<AppParaInstalar> apps)
        {
            InstallationState(EnumState.Installing);

            List<string> appsJaInstalados = new();

            try
            {
                for (int i = 1; i <= apps.Count; i++)
                {
                    string appCodeId = apps[i - 1].CodeId;
                    string appName = apps[i - 1].Name;

                    // Verificar se o app já está instalado
                    BarraDeProgresso.IsIndeterminate = true;
                    TextoDetalhes.Text = $"{appName} ({i - 1}/{apps.Count})";
                    string result = await Command.Executar("cmd.exe", $"/c; {Command.wingetExe} install {appCodeId}");

                    // Se der algum erro na instalacao
                    if (appsJaInstalados.Count > 0) throw new Exception($"Error installing {appName}.");

                    BarraDeProgresso.IsIndeterminate = false;
                    BarraDeProgresso.Value = (i * 100) / apps.Count;
                    TextoDetalhes.Text = $"{appName} ({i}/{apps.Count})";
                }

                InstallationState(EnumState.Installing);
            }
            catch (Exception ex)
            {
                InstallationState(EnumState.Error, ex.Message);
            }
        }
    }
}
