using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using InstallyApp.Application.Functions;
using InstallyApp.Components.Layout;

namespace InstallyApp.Components.Popups
{
    public partial class JanelaDeInstalacaoDeApps : UserControl
    {

        public List<AppParaInstalar> ListaDeAppParaInstalar { get; set; }

        public JanelaDeInstalacaoDeApps()
        {
            InitializeComponent();
            DataContext = this;
            ListaDeAppParaInstalar = new();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Master.Main.AreaDePopups.Children.Clear();
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
                        Botoes.Visibility = Visibility.Visible;
                        ConfirmarTextBlock.Text = "✔️";
                        Titulo.Visibility = Visibility.Collapsed;
                        TextoDetalhes.FontSize = 16;

                        Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Visibility = Visibility.Collapsed;

                        throw new Exception($"All selected apps are already installed and updated");
                    }

                    if (appsJaInstalados.Count > 0) throw new Exception($"{appsJaInstalados.Count} app{(appsJaInstalados.Count > 1 ? "s" : "")} is already installed \n continue the installation?");

                    IniciarInstalacao(ListaDeAppParaInstalar);
                }
                catch (Exception ex)
                {
                    Titulo.Text = ex.Message;
                    BarraDeProgresso.Visibility = Visibility.Collapsed;
                    TextoDetalhes.Visibility = Visibility.Collapsed;
                    Botoes.Visibility = Visibility.Visible;
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
                }
            }
            else
            {
                Titulo.Visibility = Visibility.Collapsed;
                BarraDeProgresso.Visibility = Visibility.Collapsed;
                TextoDetalhes.FontSize = 18;
                ConfirmarTextBlock.Text = "✔️";
                Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Visibility = Visibility.Collapsed;

                TextoDetalhes.Text = "Select at least one app to install";
                TextoDetalhes.Width = 300;
            }
        }

        public async void IniciarInstalacao(List<AppParaInstalar> apps)
        {
            Titulo.Text = "Installing...";

            Titulo.Visibility = Visibility.Visible;
            Botoes.Visibility = Visibility.Collapsed;
            BarraDeProgresso.Visibility = Visibility.Visible;
            BarraDeProgresso.IsIndeterminate = false;
            BarraDeProgresso.Value = 0;

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

                Titulo.Text = "All apps successfully installed!";
                TextoDetalhes.Visibility = Visibility.Collapsed;
                Botoes.Visibility = Visibility.Visible;
                ConfirmarTextBlock.Text = "✔️";
                Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Titulo.Text = "Installation error";
                BarraDeProgresso.Visibility = Visibility.Collapsed;
                TextoDetalhes.Text = ex.Message;
                Botoes.Visibility = Visibility.Visible;
                Confirmar.MouseDown += (object sender, MouseButtonEventArgs e) => App.Master.Main.AreaDePopups.Children.Clear();
                {
                    // Continuar a instalação ?
                    Botoes.Visibility = Visibility.Collapsed;
                    Titulo.Text = "Installing...";
                    BarraDeProgresso.Visibility = Visibility.Visible;
                    TextoDetalhes.Text = "Installing...";
                };
            }
        }
    }
}
